using FluentAssertions;
using NSubstitute;
using SimpleShopD.Application.Commands.Users.Login;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Services.DTO;
using SimpleShopD.Domain.Users;
using SimpleShopD.Domain.Users.Exceptions;
using SimpleShopD.Domain.Users.ValueObjects;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Tests.Commands.Users
{
    public class LoginUserTests
    {
        private readonly ICommandTResultHandler<LoginUser, AuthResponse> _commandHandler;
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenProvider;
        private readonly IContextAccessor _contextAccessor;

        public LoginUserTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _tokenProvider = Substitute.For<ITokenProvider>();
            _contextAccessor = Substitute.For<IContextAccessor>();
            _commandHandler = new LoginUserCommand(_userRepository, _tokenProvider, _contextAccessor);
        }

        private async Task<AuthResponse> Act(LoginUser command)
            => await _commandHandler.HandleAsync(command);

        [Fact]
        public async Task LoginUser_ForNotExistingUser_ShouldThrowUserDoesNotExistException()
        {
            // Arrange
            string email = "john.doe@ssd.com";
            string password = "JohnDoe123!";
            var command = new LoginUser(email, password);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<UserDoesNotExistException>();
            exception.Message.Should().Be(email);
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task LoginUser_ForInvalidPassword_ShouldThrowLoginOperationException()
        {
            // Arrange
            string email = "john.doe@ssd.com";
            string password = "JohnDoe123!WrongPassword:/";
            var command = new LoginUser(email, password);
            var user = TestUserExtension.CreateValidUser(Role.User);
            _userRepository.GetByEmailAsync(email).Returns(user);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<LoginOperationException>();
            exception.Message.Should().Be("Invalid password");
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task LoginUser_ForNotActiveAccount_ShouldThrowLoginOperationException()
        {
            // Arrange
            string email = "john.doe@ssd.com";
            string password = "JohnDoe123!";
            var command = new LoginUser(email, password);
            var user = TestUserExtension.CreateValidUser(Role.User);
            _userRepository.GetByEmailAsync(email).Returns(user);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<LoginOperationException>();
            exception.Message.Should().Be("Account is not active");
            await _userRepository.Received(0).UpdateAsync(Arg.Any<User>());
        }

        [Fact]  
        public async Task LoginUser_ForValidData_ShouldReturnAuthResponse()
        {
            // Arrange
            string email = "john.doe@ssd.com";
            string password = "JohnDoe123!";
            var command = new LoginUser(email, password);
            var user = TestUserExtension.CreateValidUser(Role.User);
            user.Activate(user.ActivationToken!.Value);
            _userRepository.GetByEmailAsync(email).Returns(user);
            _tokenProvider.Provide(user.UserRole, user.Id, user.Email.EmailAddress).Returns("some jwt");
            _tokenProvider.GenerateRandomToken().Returns("some refresh");

            // Act
            var result = await Act(command);

            // Assert
            result.Should().NotBeNull();
            result.Jwt.Should().NotBeEmpty();
            result.Fullname.Should().NotBeEmpty();
            await _userRepository.Received(1).UpdateAsync(Arg.Any<User>());
            user.RefreshToken.Should().NotBeNull();
        }
    }
}
