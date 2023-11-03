using FluentAssertions;
using NSubstitute;
using SimpleShopD.Application.Commands.Users.RoleChange;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Users.ValueObjects;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Tests.Commands.Users
{
    public class ChangeRoleTests
    {
        private readonly ICommandHandler<ChangeRole> _handler;
        private readonly IUserRepository _userRepository;

        public ChangeRoleTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new ChangeRoleCommand(_userRepository);
        }
        private async Task Act(ChangeRole command)
            => await _handler.HandleAsync(command);

        [Fact]
        public async Task ChangeRole_ForNotExistingUser_ShouldThrowUserDoesNotExistException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            var command = new ChangeRole(userId, Role.User);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<UserDoesNotExistException>();
            exception.Message.Should().Be(userId.ToString());
        }

        [Fact]
        public async Task ChangeRole_ForExistingUser_ShouldNotThrowException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            var command = new ChangeRole(userId, Role.User);
            _userRepository.GetAsync(userId).Returns(TestUserExtension.CreateValidUser(Role.User));

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeNull();
        }
    }
}
