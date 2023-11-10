using FluentAssertions;
using SimpleShopD.Domain.Addresses.Exceptions;
using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Shared.Exceptions;
using SimpleShopD.Domain.Shared.ValueObjects;
using SimpleShopD.Domain.Users;
using SimpleShopD.Domain.Users.Exceptions;
using SimpleShopD.Domain.Users.ValueObjects;

namespace SimpleShopD.Domain.Tests
{
    public class UserTests
    {
        [Fact]
        public void NewUser_ForEmptyFirstName_ShouldThrowEmptyFullnameException()
        {
            // Arrange
            string firstName = string.Empty;
            string lastName = "Doe";
            string emailAddress = "john.doe@ssd.com";
            string password = "12345Abc@";
            string token = GenerateRandomText(64);

            // Act
            var exception = Record.Exception(() => new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, Role.User, token));

            // Assert
            exception.Should().BeOfType<EmptyFullnameException>();
            exception.Message.Should().Be("Firstname cannot be empty");
        }

        [Fact]
        public void NewUser_ForEmptyLastName_ShouldThrowEmptyFullnameException()
        {
            // Arrange
            string firstName = "John";
            string lastName = string.Empty;
            string emailAddress = "john.doe@ssd.com";
            string password = "12345Abc@";
            string token = GenerateRandomText(64);

            // Act
            var exception = Record.Exception(() => new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, Role.User, token));

            // Assert
            exception.Should().BeOfType<EmptyFullnameException>();
            exception.Message.Should().Be("Lastname cannot be empty");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1000)]
        public void NewUser_ForOutOfLengthRangeFirstName_ShouldThrowEmptyFullnameException(int firstNameLength)
        {
            // Arrange
            string firstName = GenerateRandomText(firstNameLength);
            string lastName = "Doe";
            string emailAddress = "john.doe@ssd.com";
            string password = "12345Abc@";
            string token = GenerateRandomText(64);

            // Act
            var exception = Record.Exception(() => new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, Role.User, token));

            // Assert
            exception.Should().BeOfType<InvalidFullnameLengthException>();
            exception.Message.Should().Be("Firstname length must be longer than 2 and shorter than 100");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1000)]
        public void NewUser_ForOutOfLengthRangeLastName_ShouldThrowEmptyFullnameException(int lastNameLength)
        {
            // Arrange
            string firstName = "John";
            string lastName = GenerateRandomText(lastNameLength);
            string emailAddress = "john.doe@ssd.com";
            string password = "12345Abc@";
            string token = GenerateRandomText(64);

            // Act
            var exception = Record.Exception(() => new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, Role.User, token));

            // Assert
            exception.Should().BeOfType<InvalidFullnameLengthException>();
            exception.Message.Should().Be("Lastname length must be longer than 2 and shorter than 100");
        }

        [Fact]
        public void NewUser_ForInvalidEmail_ShouldThrowInvalidEmailException()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            string emailAddress = "JohnDoe";
            string password = "12345Abc@";
            string token = GenerateRandomText(64);

            // Act
            var exception = Record.Exception(() => new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, Role.User, token));

            // Assert
            exception.Should().BeOfType<InvalidEmailException>();
            exception.Message.Should().Be($"The provided value {emailAddress} is not an email address");
        }

        [Fact]
        public void NewUser_ForTooLongEmail_ShouldThrowInvalidEmailException()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            string emailAddress = GenerateRandomText(500) + "@company.com";
            string password = "12345Abc@";
            string token = GenerateRandomText(64);

            // Act
            var exception = Record.Exception(() => new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, Role.User, token));

            // Assert
            exception.Should().BeOfType<InvalidEmailException>();
            exception.Message.Should().Be($"Email length must be shorter than 400");
        }

        [Fact]
        public void NewUser_ForEmptyPassword_ShouldThrowPasswordPolicyException()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            string emailAddress = "john.doe@ssd.com";
            string password = string.Empty;
            string token = GenerateRandomText(64);

            // Act
            var exception = Record.Exception(() => new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, Role.User, token));

            // Assert
            exception.Should().BeOfType<PasswordPolicyException>();
            exception.Message.Should().Be($"The password does not meet security requirements. It should have a minimum of 8 characters, contain lowercase and uppercase letters, numbers and special characters");
        }

        [Fact]
        public void NewUser_ForIncorrectPassword_ShouldThrowPasswordPolicyException()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            string emailAddress = "john.doe@ssd.com";
            string password = "JohnDoe1222334234234";
            string token = GenerateRandomText(64);

            // Act
            var exception = Record.Exception(() => new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, Role.User, token));

            // Assert
            exception.Should().BeOfType<PasswordPolicyException>();
            exception.Message.Should().Be($"The password does not meet security requirements. It should have a minimum of 8 characters, contain lowercase and uppercase letters, numbers and special characters");
        }

        [Theory]
        [InlineData("", "Warsaw", "00-000", "Złota", "1", "Country cannot be empty", typeof(EmptyAddressParameterException))]
        [InlineData("Poland", "", "00-000", "Złota", "1", "City cannot be empty", typeof(EmptyAddressParameterException))]
        [InlineData("Poland", "Warsaw", "", "Złota", "1", "Zipcode cannot be empty", typeof(EmptyAddressParameterException))]
        [InlineData("Poland", "Warsaw", "00-000", "", "1", "Street cannot be empty", typeof(EmptyAddressParameterException))]
        [InlineData("Poland", "Warsaw", "00-000", "Złota", "", "Building number cannot be empty", typeof(EmptyAddressParameterException))]
        public void NewUser_ForInvalidAddress_ShouldThrowEmptyAddressException(string country, string city, string zipCode, string street, string buildingNumber, string message, Type exceptionType)
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            string emailAddress = "john.doe@ssd.com";
            string password = "JohnDoe123!";
            string token = GenerateRandomText(64);

            // Act
            var user = new User(Guid.NewGuid(), new Fullname(firstName, lastName), emailAddress, password, Role.User, token);
            var exception = Record.Exception(() => user.AddAddress(Guid.NewGuid(), country, city, zipCode, street, buildingNumber));

            // Assert
            exception.Should().BeOfType(exceptionType);
            exception.Message.Should().Be(message);
        }

        [Fact]
        public void NewUser_ForValidData_ShouldCreateANewUser()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            string emailAddress = "john.doe@ssd.com";
            string password = "JohnDoe123!";
            string token = GenerateRandomText(64);

            // Act
            var user = new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, Role.User, token);
            user.AddAddress(Guid.NewGuid(), "Poland", "Warsaw", "00-000", "Złota", "1");
            user.AddAddress(Guid.NewGuid(), "Poland", "Warsaw", "00-000", "Złota", "2");

            // Assert
            user.Addresses.Take(1).First().Country.Value.Should().Be("Poland");
            user.Addresses.Take(1).First().City.Value.Should().Be("Warsaw");
            user.Addresses.Take(1).First().ZipCode.Value.Should().Be("00-000");
            user.Addresses.Take(1).First().Street.Value.Should().Be("Złota");
            user.Addresses.Take(1).First().BuildingNumber.Value.Should().Be("1");
            user.Addresses.Take(2).Skip(1).First().Country.Value.Should().Be("Poland");
            user.Addresses.Take(2).Skip(1).First().City.Value.Should().Be("Warsaw");
            user.Addresses.Take(2).Skip(1).First().ZipCode.Value.Should().Be("00-000");
            user.Addresses.Take(2).Skip(1).First().Street.Value.Should().Be("Złota");
            user.Addresses.Take(2).Skip(1).First().BuildingNumber.Value.Should().Be("2");
            user.Fullname.Firstname.Should().Be(firstName);
            user.Fullname.Lastname.Should().Be(lastName);
            user.Email.EmailAddress.Should().Be(emailAddress);
            user.Password.VerifyPassword(password).Should().BeTrue();
            user.Status.Value.Should().Be(AccountStatus.Inactive);
            user.UserRole.Value.Should().Be(Role.User);
            user.ActivationToken!.Value.Should().NotBeNull();
            user.ResetPasswordToken.Should().BeNull();
            user.RefreshToken.Should().BeNull();
        }

        private static string GenerateRandomText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }

            return new string(result);
        }
    }
}
