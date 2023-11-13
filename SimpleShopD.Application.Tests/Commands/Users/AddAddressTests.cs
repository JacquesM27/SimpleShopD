using FluentAssertions;
using NSubstitute;
using SimpleShopD.Application.Commands.Users.AddressAdd;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Users;
using SimpleShopD.Domain.Users.ValueObjects;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Tests.Commands.Users
{
    public class AddAddressTests
    {
        private readonly ICommandTResultHandler<AddAddress, Guid> _handler;
        private readonly IUserRepository _userRepository;
        private readonly IContextAccessor _contextAccessor;

        public AddAddressTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _contextAccessor = Substitute.For<IContextAccessor>();
            _handler = new AddAddressCommand(_userRepository, _contextAccessor);
        }

        private async Task Act(AddAddress command)
            => await _handler.HandleAsync(command);

        [Fact]
        public async Task AddAddress_ForNotExistingUser_ShouldThrowUserDoesNotExistException()
        {
            // Arrange
            var address = new Dto.AddressDto("Poland", "Warsaw", "09-009", "Złota", "63");
            var command = new AddAddress(address);

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));

            // Assert
            exception.Should().BeOfType<UserDoesNotExistException>();
            exception.Message.Should().Be(Guid.Empty.ToString());
        }

        [Fact]
        public async Task AddAddress_ForValidAddress_ShouldAddAddress()
        {
            // Arrange
            var address = new Dto.AddressDto("Poland", "Warsaw", "09-009", "Złota", "63");
            var command = new AddAddress(address);
            var user = TestUserExtension.CreateValidUser(Role.User);
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
