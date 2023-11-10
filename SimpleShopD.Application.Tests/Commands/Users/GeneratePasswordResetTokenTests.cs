using FluentAssertions;
using NSubstitute;
using SimpleShopD.Application.Commands.Users.ResetPasswordToken;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Users.ValueObjects;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Tests.Commands.Users
{
    public class GeneratePasswordResetTokenTests
    {
        private readonly ICommandHandler<GeneratePasswordResetToken> _handler;
        private readonly IUserRepository _userRepository;
        private readonly IContextAccessor _contextAccessor;
        private readonly ITokenProvider _tokenProvider;

        public GeneratePasswordResetTokenTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _contextAccessor = Substitute.For<IContextAccessor>();
            _tokenProvider = Substitute.For<ITokenProvider>();
            _handler = new GeneratePasswordResetTokenCommand(_userRepository, _contextAccessor, _tokenProvider);
        }
        private async Task Act(GeneratePasswordResetToken command)
            => await _handler.HandleAsync(command);

        [Fact]
        public async Task GeneratePasswordResetToken_ForNotExistingUser_ShouldThrowUserDoesNotExistException()
        {
            // Arrange
            string email = "john.doe@ssd.com";
            var command = new GeneratePasswordResetToken(email);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<UserDoesNotExistException>();
            exception.Message.Should().Be(Guid.Empty.ToString());
        }

        [Fact]
        public async Task GeneratePasswordResetToken_ForExistingUser_ShouldNotThrowException()
        {
            // Arrange
            string email = "john.doe@ssd.com";
            var command = new GeneratePasswordResetToken(email);
            _tokenProvider.GenerateRandomToken().Returns(TestUserExtension.GenerateRandomToken());
            _userRepository.GetAsync(Guid.Empty).Returns(TestUserExtension.CreateValidUser(Role.User));

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeNull();
        }
    }
}
