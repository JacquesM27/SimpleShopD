using FluentAssertions;
using SimpleShopD.Domain.Orders;
using SimpleShopD.Domain.Shared.Exceptions;
using SimpleShopD.Domain.Shared.ValueObjects;

namespace SimpleShopD.Domain.Tests
{
    public class OrderTests
    {
        [Fact]
        public void AddNewOrder_ForInvalidFullname_ShouldThrowFullnameException()
        {
            Guid orderId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            Guid addressId = Guid.NewGuid();
            string firstName = string.Empty;
            string lastName = "Doe";

            // Act
            var exception = Record.Exception(() => new Order(orderId, userId, addressId, new Fullname(firstName, lastName)));

            // Assert
            exception.Should().BeOfType<EmptyFullnameException>();
            exception.Message.Should().Be("Firstname cannot be empty");
        }

        [Fact]
        public void AddNewOrder_ForValidData_ShouldCreateNewOrder()
        {
            Guid orderId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            Guid addressId = Guid.NewGuid();
            string firstName = "John";
            string lastName = "Doe";

            // Act
            var order = new Order(orderId, userId, addressId, new Fullname(firstName, lastName));

            // Assert
            order.Id.Should().Be(orderId);
            order.UserId.Should().Be(userId);
            order.ReceiverFullname.Firstname.Should().Be(firstName);
            order.ReceiverFullname.Lastname.Should().Be(lastName);
            order.OrderLines.Count.Should().Be(0);
            order.AddressId.Should().Be(addressId);
        }
    }
}
