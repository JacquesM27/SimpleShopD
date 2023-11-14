using FluentAssertions;
using NSubstitute;
using SimpleShopD.Application.Commands.Users.ChangeUserPassword;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Users;
using SimpleShopD.Domain.Users.Exceptions;
using SimpleShopD.Domain.Users.ValueObjects;
using SimpleShopD.Shared.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShopD.Application.Tests.Commands.Users
{
    public class ChangePasswordTests
    {
        private readonly ICommandHandler<ChangePassword> _commandHandler;
        private readonly IUserRepository _userRepository;
        private readonly IContextAccessor _contextAccessor;

        public ChangePasswordTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _contextAccessor = Substitute.For<IContextAccessor>();
            _commandHandler = new ChangePasswordCommand(_userRepository, _contextAccessor);
        }

        private async Task Act(ChangePassword command)
            => await _commandHandler.HandleAsync(command);

        [Fact]
        public async Task ChangePassword_ForNotExistingUser_ShouldThrowUserDoesNotExistException()
        {
            // Arrange
            var command = new ChangePassword("JohnDoe123!", "JohnDoe123!");

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<UserDoesNotExistException>();
            exception.Message.Should().Be(Guid.Empty.ToString());
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task ChangePassword_ForInvalidCurrentPassword_ShouldThrowChangingPasswordException()
        {
            // Arrange
            var user = TestUserExtension.CreateValidUser(Role.User);
            _userRepository.GetAsync(Guid.Empty).Returns(user);
            string oldValidPassword = "JohnDoe123!";
            string oldInvalidPassword = "JonhDoe123@";
            string newPassword = "JohnDoe123!!";
            var command = new ChangePassword(oldInvalidPassword, newPassword);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            user.Password.VerifyPassword(oldValidPassword).Should().BeTrue();
            user.Password.VerifyPassword(newPassword).Should().BeFalse();
            exception.Should().BeOfType<ChangingPasswordException>();
            exception.Message.Should().Be("Invalid old password");
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task ChangePassword_ForInvalidPasswordPolicy_ShouldThrowPasswordPolicyException()
        {
            // Arrange
            var user = TestUserExtension.CreateValidUser(Role.User);
            _userRepository.GetAsync(Guid.Empty).Returns(user);
            string oldPassword = "JohnDoe123!";
            string newPassword = "JohnDoe";
            var command = new ChangePassword(oldPassword, newPassword);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            user.Password.VerifyPassword(oldPassword).Should().BeTrue();
            user.Password.VerifyPassword(newPassword).Should().BeFalse();
            exception.Should().BeOfType<PasswordPolicyException>();
            exception.Message.Should().Be($"The password does not meet security requirements. It should have a minimum of 8 characters, contain lowercase and uppercase letters, numbers and special characters");
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task ChangePassword_ForValidData_ShouldChangeUserPassword()
        {
            // Arrange
            var user = TestUserExtension.CreateValidUser(Role.User);
            _userRepository.GetAsync(Guid.Empty).Returns(user);
            string oldPassword = "JohnDoe123!";
            string newPassword = "JohnDoe234@";
            var command = new ChangePassword(oldPassword, newPassword);

            // Act
            await Act(command);

            // Assert
            user.Password.VerifyPassword(oldPassword).Should().BeFalse();
            user.Password.VerifyPassword(newPassword).Should().BeTrue();
            await _userRepository.Received(1).UpdateAsync(Arg.Any<User>());
        }
    }
}
