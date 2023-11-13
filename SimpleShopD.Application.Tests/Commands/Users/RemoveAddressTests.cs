using FluentAssertions;
using NSubstitute;
using SimpleShopD.Application.Commands.Users.AddressRemove;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Users;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Tests.Commands.Users
{
    public class RemoveAddressTests
    {
        private readonly ICommandHandler<RemoveAddress> _commandHandler;
        private readonly IUserRepository _userRepository;
        private readonly IContextAccessor _contextAccessor;

        public RemoveAddressTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _contextAccessor = Substitute.For<IContextAccessor>();
            _commandHandler = new RemoveAddressCommand(_userRepository, _contextAccessor);
        }

        private async Task Act(RemoveAddress command)
            => await _commandHandler.HandleAsync(command);

        [Fact]
        public async Task RemoveAddress_ForNotExistingUser_ShouldThrowUserDoesNotExistException()
        {
            // Arrange
            Guid addressId = Guid.NewGuid();
            var command = new RemoveAddress(addressId);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<UserDoesNotExistException>();
            exception.Message.Should().Be(Guid.Empty.ToString());
        }

        [Fact]
        public async Task RemoveAddress_ForNotExistAddress_ShouldNotChangeUserAddresses()
        {
            // Arrange
            Guid addressId = Guid.NewGuid();
            var command = new RemoveAddress(addressId);
            var user = TestUserExtension.CreateValidUser(Domain.Users.ValueObjects.Role.User);
            user.AddAddress(Guid.NewGuid() ,"Poland", "Warsaw", "09-009", "Złota", "63");
            user.AddAddress(Guid.NewGuid() ,"Poland", "Warsaw", "09-009", "Złota", "63");
            _userRepository.GetAsync(Guid.Empty).Returns(user);

            // Act
            await Act(command);

            // Assert
            await _userRepository.Received(1).UpdateAsync(Arg.Any<User>());
            user.Addresses.Should().NotBeNull();
            user.Addresses.Count.Should().Be(2);
        }

        [Fact]
        public async Task RemoveAddress_ForVaidId_ShouldRemoveAddress()
        {
            // Arrange
            Guid addressId = Guid.NewGuid();
            var command = new RemoveAddress(addressId);
            var user = TestUserExtension.CreateValidUser(Domain.Users.ValueObjects.Role.User);
            user.AddAddress(addressId, "Poland", "Warsaw", "09-009", "Złota", "63");
            user.AddAddress(Guid.NewGuid(), "Poland", "Warsaw", "09-009", "Złota", "63");
            _userRepository.GetAsync(Guid.Empty).Returns(user);

            // Act
            await Act(command);

            // Assert
            await _userRepository.Received(1).UpdateAsync(Arg.Any<User>());
            user.Addresses.Should().NotBeNull();
            user.Addresses.Count.Should().Be(1);
        }
    }
}
