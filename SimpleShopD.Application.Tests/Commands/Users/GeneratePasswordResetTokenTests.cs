using FluentAssertions;
using NSubstitute;
using SimpleShopD.Application.Commands.Users.ResetPasswordToken;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Tests.Commands.Users
{
    public class GeneratePasswordResetTokenTests
    {
        private readonly ICommandHandler<GeneratePasswordResetToken> _handler;
        private readonly IUserRepository _userRepository;

        public GeneratePasswordResetTokenTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new GeneratePasswordResetTokenCommand(_userRepository);
        }
        private async Task Act(GeneratePasswordResetToken command)
            => await _handler.HandleAsync(command);

        [Fact]
        public async Task GeneratePasswordResetToken_ForNotExistingUser_ShouldThrowUserDoesNotExistException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            var command = new GeneratePasswordResetToken(userId);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<UserDoesNotExistException>();
            exception.Message.Should().Be(userId.ToString());
        }

        [Fact]
        public async Task GeneratePasswordResetToken_ForExistingUser_ShouldNotThrowException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            var command = new GeneratePasswordResetToken(userId);
            _userRepository.GetAsync(userId).Returns(TestUserExtension.CreateValidUser(UserRole.User));

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeNull();
        }
    }
}
