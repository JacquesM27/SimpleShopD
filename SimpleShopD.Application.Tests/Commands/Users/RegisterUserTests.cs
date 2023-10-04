﻿using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using SimpleShopD.Application.Commands.SharedDto;
using SimpleShopD.Application.Commands.Users.UserRegister;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Policies;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Users;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Tests.Commands.Users
{
    public class RegisterUserTests
    {
        private readonly ICommandTValueHandler<RegisterUser, Guid> _commandHandler;
        private readonly IUserRepository _userRepository;

        public RegisterUserTests()
        {
            var services = new ServiceCollection().ConfigurePolicies();
            var provieder = services.BuildServiceProvider();

            _userRepository = Substitute.For<IUserRepository>();
            _commandHandler = new RegisterUserCommand(_userRepository, provieder.GetServices<ICreateUserPolicy>());
        }

        private async Task<Guid> Act(RegisterUser command)
            => await _commandHandler.HandleAsync(command);

        [Fact]
        public async Task RegisterUserHandler_WithValidUser_ShouldCreateUser()
        {
            // Arrange
            var command = new RegisterUser("John", "Doe", "john@example.com", "JohnDoe123!", 
                UserRole.User, new List<Address>(), "User", null);
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
                UserRole.Admin, new List<Address>(), "User", authorId);
            _userRepository.IsTheEmailUniqueAsync(command.Email).Returns(true);
            _userRepository.GetAsync(authorId).Returns(CreateUser(UserRole.Admin));

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
                UserRole.Admin, new List<Address>(), "User", authorId);
            _userRepository.IsTheEmailUniqueAsync(command.Email).Returns(true);
            _userRepository.GetAsync(authorId).Returns(CreateUser(UserRole.User));

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
                UserRole.Admin, new List<Address>(), "User", null);
            _userRepository.IsTheEmailUniqueAsync(command.Email).Returns(true);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<CannotCreateUserPolicyException>();
            exception.Message.Should().Be(string.Empty);
        }

        private static User CreateUser(UserRole role)
        {
            string firstName = "John";
            string lastName = "Doe";
            string emailAddress = "john.doe@ssd.com";
            string password = "JohnDoe123!";

            return new(Guid.NewGuid(), new Domain.Shared.ValueObjects.Fullname(firstName,
                lastName), emailAddress, password, role, null);
        }
    }
}
