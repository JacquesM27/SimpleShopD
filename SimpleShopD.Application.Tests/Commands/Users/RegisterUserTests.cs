using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using SimpleShopD.Application.Commands.Users.UserRegister;
using SimpleShopD.Application.Dto;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Policies;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Users.ValueObjects;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Tests.Commands.Users
{
    public class RegisterUserTests
    {
        private readonly ICommandTResultHandler<RegisterUser, Guid> _commandHandler;
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenProvider;

        public RegisterUserTests()
        {
            var services = new ServiceCollection().ConfigurePolicies();
            var provieder = services.BuildServiceProvider();

            _userRepository = Substitute.For<IUserRepository>();
            _tokenProvider = Substitute.For<ITokenProvider>();
            _commandHandler = new RegisterUserCommand(_userRepository, provieder.GetServices<ICreateUserPolicy>(), _tokenProvider);
        }

        private async Task<Guid> Act(RegisterUser command)
            => await _commandHandler.HandleAsync(command);

        [Fact]
        public async Task RegisterUserHandler_WithValidUser_ShouldCreateUser()
        {
            // Arrange
            var command = new RegisterUser("John", "Doe", "john@example.com", "JohnDoe123!", 
                Role.User, new List<AddressDto>(), "User", null);
            _tokenProvider.GenerateRandomToken().Returns(TestUserExtension.GenerateRandomToken());
            _userRepository.IsTheEmailUniqueAsync(command.Email).Returns(true);

            // Act
            var userId = await Act(command);

            // Assert
            userId.Should().NotBeEmpty();
            //Assert.NotEqual(Guid.Empty, userId);
        }

        [Fact]
        public async Task RegisterAdminHandler_WithValidData_ShouldCreateUser()
        {
            // Arrange
            Guid authorId = Guid.NewGuid();
            var command = new RegisterUser("John", "Doe", "john@example.com", "JohnDoe123!",
                Role.Admin, new List<AddressDto>(), "User", authorId);
            _tokenProvider.GenerateRandomToken().Returns(TestUserExtension.GenerateRandomToken());
            _userRepository.IsTheEmailUniqueAsync(command.Email).Returns(true);
            _userRepository.GetAsync(authorId).Returns(TestUserExtension.CreateValidUser(Role.Admin));

            // Act
            var userId = await Act(command);

            // Assert
            Assert.NotEqual(Guid.Empty, userId);
        }

        [Fact]
        public async void RegisterAdminHandler_ByNonAdmin_ShouldThrowCannotCreateUserPolicyException()
        {
            // Arrange
            Guid authorId = Guid.NewGuid();
            var command = new RegisterUser("John", "Doe", "john@example.com", "JohnDoe123!",
                Role.Admin, new List<AddressDto>(), "User", authorId);
            _tokenProvider.GenerateRandomToken().Returns(TestUserExtension.GenerateRandomToken());
            _userRepository.IsTheEmailUniqueAsync(command.Email).Returns(true);
            _userRepository.GetAsync(authorId).Returns(TestUserExtension.CreateValidUser(Role.User));

            // Act
            var exception = await Record.ExceptionAsync(async () => await Act(command));

            // Assert
            exception.Should().BeOfType<CannotCreateUserPolicyException>();
            exception.Message.Should().Be(authorId.ToString());
        }

        [Fact]
        public async void RegisterAdminHandler_ByMissingAuthor_ShouldThrowCannotCreateUserPolicyException()
        {
            // Arrange
            var command = new RegisterUser("John", "Doe", "john@example.com", "JohnDoe123!",
                Role.Admin, new List<AddressDto>(), "User", null);
            _tokenProvider.GenerateRandomToken().Returns(TestUserExtension.GenerateRandomToken());
            _userRepository.IsTheEmailUniqueAsync(command.Email).Returns(true);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<CannotCreateUserPolicyException>();
            exception.Message.Should().Be(string.Empty);
        }
    }
}
