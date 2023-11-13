using FluentAssertions;
using NSubstitute;
using SimpleShopD.Application.Commands.Users.Activate;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Users;
using SimpleShopD.Domain.Users.Exceptions;
using SimpleShopD.Domain.Users.ValueObjects;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Tests.Commands.Users
{
    public class ActivateUserTests
    {
        private readonly ICommandHandler<ActivateAccount> _handler;
        private readonly IUserRepository _userRepository;

        public ActivateUserTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new ActivateAccountCommand(_userRepository);
        }

        private async Task Act(ActivateAccount command)
            => await _handler.HandleAsync(command);

        [Fact]
        public async Task ActivateUser_ForNotExistsingUser_ShouldThrowUserDoesNotExistException()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var command = new ActivateAccount(guid, string.Empty);
            _userRepository.GetAsync(Guid.Empty).Returns(TestUserExtension.CreateValidUser(Role.User));

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<UserDoesNotExistException>();
            exception.Message.Should().Be(guid.ToString());
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task ActivateUser_ForInvalidActivationToken_ShouldThrowActivationOperationException()
        {
            // Arrange
            var user = TestUserExtension.CreateValidUser(Role.User);
            var token = TestUserExtension.GenerateRandomToken() + "now I am sure that token is different";
            var command = new ActivateAccount(Guid.Empty, token);
            _userRepository.GetAsync(Guid.Empty).Returns(TestUserExtension.CreateValidUser(Role.User));

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<ActivationOperationException>();
            exception.Message.Should().Be("Invalid activation token");
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task ActivateUser_ForAlreadyActivatedUser_ShouldExecuteUpdateOneTime()
        {
            // Arrange
            var user = TestUserExtension.CreateValidUser(Role.User);
            user.Activate(user.ActivationToken!.Value);
            var command = new ActivateAccount(Guid.Empty, string.Empty);
            _userRepository.GetAsync(Guid.Empty).Returns(user);

            // Act
            await Act(command);

            // Assert
            await _userRepository.Received(1).UpdateAsync(Arg.Any<User>());
            user.Status.Value.Should().Be(Domain.Enum.AccountStatus.Active);
            user.ActivationToken.Should().BeNull();
        }

        [Fact]
        public async Task ActivateUser_ForNotActivatedUser_ShouldSetUserActivated()
        {
            // Arrange
            var user = TestUserExtension.CreateValidUser(Role.User);
            var command = new ActivateAccount(Guid.Empty, user.ActivationToken!.Value);
            _userRepository.GetAsync(Guid.Empty).Returns(user);

            // Act
            await Act(command);

            // Assert
            await _userRepository.Received(1).UpdateAsync(Arg.Any<User>());
            user.Status.Value.Should().Be(Domain.Enum.AccountStatus.Active);
            user.ActivationToken.Should().BeNull();
        }
    }
}
