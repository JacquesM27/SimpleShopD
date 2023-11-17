using FluentAssertions;
using NSubstitute;
using SimpleShopD.Application.Commands.Users.RefreshToken;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Users.Exceptions;
using SimpleShopD.Domain.Users.ValueObjects;
using SimpleShopD.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShopD.Application.Commands.Users.NewPassword;
using System.Reflection;

namespace SimpleShopD.Application.Tests.Commands.Users
{
    public class GenerateRefreshTokenTests
    {
        private readonly GenerateRefreshTokenCommand _commandHandler;
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenProvider;
        private readonly IContextAccessor _contextAccessor;

        public GenerateRefreshTokenTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _tokenProvider = Substitute.For<ITokenProvider>();
            _contextAccessor = Substitute.For<IContextAccessor>();
            _commandHandler = new GenerateRefreshTokenCommand(_userRepository, _tokenProvider, _contextAccessor);
        }

        private async Task Act(GenerateRefreshToken command)
            => await _commandHandler.HandleAsync(command);

        [Fact]
        public async Task GenerateRefreshToken_WithInvalidUserId_ShouldThrowUserDoesNotExistException()
        {
            // Arrange
            var command = new GenerateRefreshToken();
            _contextAccessor.GetUserId().Returns(Guid.Empty);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<UserDoesNotExistException>();
            exception.Message.Should().Be(Guid.Empty.ToString());
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task GenerateRefreshToken_ForNotExistingUser_ShouldThrowUserDoesNotExistException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new GenerateRefreshToken();
            _contextAccessor.GetUserId().Returns(userId);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<UserDoesNotExistException>();
            exception.Message.Should().Be(userId.ToString());
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task GenerateRefreshToken_ForNotActivatedUser_ShouldThrowRefreshTokenOperationException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new GenerateRefreshToken();
            _contextAccessor.GetUserId().Returns(userId);
            var user = TestUserExtension.CreateValidUser(Role.User);
            _userRepository.GetAsync(userId).Returns(user);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<RefreshTokenOperationException>();
            exception.Message.Should().Be("Account is not active");
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task GenerateRefreshToken_ForUserNullRefresh_ShouldThrowRefreshTokenOperationException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new GenerateRefreshToken();
            _contextAccessor.GetUserId().Returns(userId);
            var user = TestUserExtension.CreateValidUser(Role.User);
            user.Activate(user.ActivationToken!.Value);
            _userRepository.GetAsync(userId).Returns(user);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<RefreshTokenOperationException>();
            exception.Message.Should().Be("Missing refresh");
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task GenerateRefreshToken_ForRefreshExpired_ShouldThrowRefreshTokenOperationException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new GenerateRefreshToken();
            string refreshToken = TestUserExtension.GenerateRandomToken();
            _tokenProvider.GenerateRandomToken().Returns(refreshToken);
            _contextAccessor.GetUserId().Returns(userId);
            _contextAccessor.GetRefreshToken().Returns(refreshToken);
            var user = TestUserExtension.CreateValidUser(Role.User);
            user.Activate(user.ActivationToken!.Value);
            user.Login("JohnDoe123!", _tokenProvider, _contextAccessor);
            //user.RefreshToken!.IsExpired.Returns(true);

            _userRepository.GetAsync(userId).Returns(user);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<RefreshTokenOperationException>();
            exception.Message.Should().Be("Refresh is expired");
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

    }
}
