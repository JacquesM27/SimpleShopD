using FluentAssertions;
using SimpleShopD.Domain.Orders;
using SimpleShopD.Domain.Shared.Exceptions;
using SimpleShopD.Domain.Shared.ValueObjects;

namespace SimpleShopD.Domain.Tests
{
    public class OrderTests
    {
        [Theory]
        [InlineData("", "Warsaw", "00-000", "Złota", "1", "Country cannot be empty", typeof(EmptyAddressParameterException))]
        [InlineData("Poland", "", "00-000", "Złota", "1", "City cannot be empty", typeof(EmptyAddressParameterException))]
        [InlineData("Poland", "Warsaw", "", "Złota", "1", "Zipcode cannot be empty", typeof(EmptyAddressParameterException))]
        [InlineData("Poland", "Warsaw", "00-000", "", "1", "Street cannot be empty", typeof(EmptyAddressParameterException))]
        [InlineData("Poland", "Warsaw", "00-000", "Złota", "", "Building number cannot be empty", typeof(EmptyAddressParameterException))]
        public void AddNewOrder_ForInvalidAddress_ShouldThrowEmptyAddressParameterException(string country, string city, string zipCode, string street, string buildingNumber, string message, Type exceptionType)
        {
            // Arrange
            Guid orderId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string firstName = "John";
            string lastName = "Doe";

            // Act
            var exception = Record.Exception(() => new Order(orderId, userId, new Address(country, city, zipCode, street, buildingNumber), new Fullname(firstName, lastName)));

            // Assert
            exception.Should().BeOfType(exceptionType);
            exception.Message.Should().Be(message);
        }

        [Fact]
        public void AddNewOrder_ForInvalidFullname_ShouldThrowFullnameException()
        {
            Guid orderId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string firstName = string.Empty;
            string lastName = "Doe";
            string country = "Poland";
            string city = "Warsaw";
            string zipCode = "00-000";
            string street = "Złota";
            string buildingNumber = "1";

            // Act
            var exception = Record.Exception(() => new Order(orderId, userId, new Address(country, city, zipCode, street, buildingNumber), new Fullname(firstName, lastName)));

            // Assert
            exception.Should().BeOfType<EmptyFullnameException>();
            exception.Message.Should().Be("Firstname cannot be empty");
        }

        [Fact]
        public void AddNewOrder_ForValidData_ShouldCreateNewOrder()
        {
            Guid orderId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string firstName = "John";
            string lastName = "Doe";
            string country = "Poland";
            string city = "Warsaw";
            string zipCode = "00-000";
            string street = "Złota";
            string buildingNumber = "1";

            // Act
            var order = new Order(orderId, userId, new Address(country, city, zipCode, street, buildingNumber), new Fullname(firstName, lastName));

            // Assert
            order.Id.Should().Be(orderId);
            order.UserId.Should().Be(userId);
            order.ReceiverFullname.Firstname.Should().Be(firstName);
            order.ReceiverFullname.Lastname.Should().Be(lastName);
            order.DeliveryAddress.Country.Should().Be(country);
            order.DeliveryAddress.City.Should().Be(city);
            order.DeliveryAddress.ZipCode.Should().Be(zipCode);
            order.DeliveryAddress.Street.Should().Be(street);
            order.DeliveryAddress.BuildingNumber.Should().Be(buildingNumber);
            order.OrderLines.Count.Should().Be(0);
        }
    }
}
