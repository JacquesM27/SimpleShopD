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
using SimpleShopD.Domain.Services.DTO;

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

        private async Task<AuthResponse> Act(GenerateRefreshToken command)
            => await _commandHandler.HandleAsync(command);

        [Fact]
        public async Task GenerateRefreshToken_ForMissingRefreshToken_ShouldThrowUserDoesNotExistException()
        {
            // Arrange
            var command = new GenerateRefreshToken();

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<MissingRefreshTokenException>();
            exception.Message.Should().Be(string.Empty);
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task GenerateRefreshToken_ForIncorrectRefresh_ShouldThrowUserDoesNotExistException()
        {
            // Arrange
            var refresh = TestUserExtension.GenerateRandomToken();
            var command = new GenerateRefreshToken();
            _contextAccessor.GetRefreshToken().Returns(refresh);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<UserDoesNotExistException>();
            exception.Message.Should().Be(refresh);
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task GenerateRefreshToken_ForNotActivatedUser_ShouldThrowRefreshTokenOperationException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new GenerateRefreshToken();
            var refresh = TestUserExtension.GenerateRandomToken();
            _contextAccessor.GetRefreshToken().Returns(refresh);
            var user = TestUserExtension.CreateValidUser(Role.User);
            _userRepository.GetByRefresh(refresh).Returns(user);

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
            var refresh = TestUserExtension.GenerateRandomToken();
            _contextAccessor.GetRefreshToken().Returns(refresh);
            var user = TestUserExtension.CreateValidUser(Role.User);
            user.Activate(user.ActivationToken!.Value);
            _userRepository.GetByRefresh(refresh).Returns(user);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<RefreshTokenOperationException>();
            exception.Message.Should().Be("Missing refresh");
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task GenerateRefreshToken_ForValidRefresh_ShouldReturnAuthResposeWithNewJwt()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new GenerateRefreshToken();
            var refresh = TestUserExtension.GenerateRandomToken();
            _contextAccessor.GetRefreshToken().Returns(refresh);
            var user = TestUserExtension.CreateValidUser(Role.User);
            user.Activate(user.ActivationToken!.Value);
            string newRefresh = TestUserExtension.GenerateRandomToken();
            _tokenProvider.GenerateRandomToken().Returns(newRefresh);
            _tokenProvider.Provide(user.UserRole, user.Id, user.Email).Returns("REAL JWT");
            user.Login("JohnDoe123!", _tokenProvider, _contextAccessor);
            _userRepository.GetByRefresh(refresh).Returns(user);
            _userRepository.GetAsync(userId).Returns(user);
            
            // Act
            var result = await Act(command);

            // Assert
            result.Should().BeOfType<AuthResponse>();
            result.Should().NotBeNull();
            result.Jwt.Should().NotBeNullOrEmpty();
            result.Fullname.Should().NotBeNullOrEmpty();
            _contextAccessor.Received(1).AppendRefreshToken(user.RefreshToken!.Value, user.RefreshToken!.ExpirationDate);
            await _userRepository.Received(1).UpdateAsync(Arg.Any<User>());
        }
    }
}
