using FluentAssertions;
using NSubstitute;
using SimpleShopD.Application.Commands.Users.Activate;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
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
        }

    }
}
