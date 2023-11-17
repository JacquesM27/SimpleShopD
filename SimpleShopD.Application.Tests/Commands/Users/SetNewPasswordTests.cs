using FluentAssertions;
using NSubstitute;
using SimpleShopD.Application.Commands.Users.NewPassword;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Users;
using SimpleShopD.Domain.Users.Exceptions;
using SimpleShopD.Domain.Users.ValueObjects;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Tests.Commands.Users
{
    public class SetNewPasswordTests
    {
        private readonly ICommandHandler<SetNewPassword> _commandHandler;
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenProvider;

        public SetNewPasswordTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _commandHandler = new SetNewPasswordCommand(_userRepository);
            _tokenProvider = Substitute.For<ITokenProvider>();
        }

        private async Task Act(SetNewPassword command)
            => await _commandHandler.HandleAsync(command);

        [Fact]
        public async Task SetNewPassword_ForNotExistingUser_ShouldThrowUserDoesNotExistException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string newPassword = "JohnDoe234@";
            string resetPasswordToken = TestUserExtension.GenerateRandomToken();
            var command = new SetNewPassword(userId, newPassword, resetPasswordToken);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<UserDoesNotExistException>();
            exception.Message.Should().Be(userId.ToString());
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task SetNewPassword_ForNullResetPasswordToken_ShouldThrowSettingNewPasswordException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string newPassword = "JohnDoe234@";
            string resetPasswordToken = TestUserExtension.GenerateRandomToken();
            var command = new SetNewPassword(userId, newPassword, resetPasswordToken);
            var user = TestUserExtension.CreateValidUser(Role.User);
            _userRepository.GetAsync(userId).Returns(user);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<SettingNewPasswordException>();
            exception.Message.Should().Be("Missing reset password token");
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task SetNewPassword_ForInvalidResetPasswordToken_ShouldThrowSettingNewPasswordException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string newPassword = "JohnDoe234@";
            string resetPasswordToken = TestUserExtension.GenerateRandomToken() + "It's certainly different now";
            var command = new SetNewPassword(userId, newPassword, resetPasswordToken);
            var user = TestUserExtension.CreateValidUser(Role.User);
            _tokenProvider.GenerateRandomToken().Returns(TestUserExtension.GenerateRandomToken());
            user.GeneratePasswordResetToken(_tokenProvider);
            _userRepository.GetAsync(userId).Returns(user);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<SettingNewPasswordException>();
            exception.Message.Should().Be("Invalid reset password token");
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task SetNewPassword_ForInvalidPasswordPolicy_ShouldThrowPasswordPolicyException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string newPassword = "JohnDoe";
            string resetPasswordToken = TestUserExtension.GenerateRandomToken();
            var command = new SetNewPassword(userId, newPassword, resetPasswordToken);
            var user = TestUserExtension.CreateValidUser(Role.User);
            _tokenProvider.GenerateRandomToken().Returns(resetPasswordToken);
            user.GeneratePasswordResetToken(_tokenProvider);
            _userRepository.GetAsync(userId).Returns(user);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<PasswordPolicyException>();
            exception.Message.Should().Be($"The password does not meet security requirements. It should have a minimum of 8 characters, contain lowercase and uppercase letters, numbers and special characters");
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task SetNewPassword_ForValidData_ShouldChangePassword()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string oldPassword = "JohnDoe123!";
            string newPassword = "JohnDoe234@";
            string resetPasswordToken = TestUserExtension.GenerateRandomToken();
            var command = new SetNewPassword(userId, newPassword, resetPasswordToken);
            var user = TestUserExtension.CreateValidUser(Role.User);
            _tokenProvider.GenerateRandomToken().Returns(resetPasswordToken);
            user.GeneratePasswordResetToken(_tokenProvider);
            _userRepository.GetAsync(userId).Returns(user);

            // Act
            await Act(command);

            // Assert
            await _userRepository.Received(1).UpdateAsync(Arg.Any<User>());
            user.Password.VerifyPassword(newPassword).Should().BeTrue();
            user.Password.VerifyPassword(oldPassword).Should().BeFalse();
            user.ResetPasswordToken.Should().BeNull();
        }
    }
}
