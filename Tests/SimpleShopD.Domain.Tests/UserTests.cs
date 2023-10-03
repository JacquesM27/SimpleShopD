using FluentAssertions;
using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Shared.Exceptions;
using SimpleShopD.Domain.Shared.ValueObjects;
using SimpleShopD.Domain.Users;
using SimpleShopD.Domain.Users.Exceptions;
using System.Net.Mail;

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

            // Act
            var exception = Record.Exception(() => new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, UserRole.User, null));

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

            // Act
            var exception = Record.Exception(() => new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, UserRole.User, null));

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

            // Act
            var exception = Record.Exception(() => new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, UserRole.User, null));

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

            // Act
            var exception = Record.Exception(() => new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, UserRole.User, null));

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

            // Act
            var exception = Record.Exception(() => new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, UserRole.User, null));

            // Assert
            exception.Should().BeOfType<InvalidEmailException>();
            exception.Message.Should().Be($"The provided value {emailAddress} is not an email address");
        }

        [Fact]
        public void NewUser_ForEmptyPassword_ShouldThrowPasswordPolicyException()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            string emailAddress = "john.doe@ssd.com";
            string password = string.Empty;

            // Act
            var exception = Record.Exception(() => new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, UserRole.User, null));

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

            // Act
            var exception = Record.Exception(() => new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, UserRole.User, null));

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

            // Act
            var exception = Record.Exception(() => new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, UserRole.User, new HashSet<Address>() { new Address(country, city, zipCode, street, buildingNumber) }));

            // Assert
            exception.Should().BeOfType(exceptionType);
            exception.Message.Should().Be(message);
        }

        [Fact]
        public void NewUser_ForDuplicatedAddresses_ShouldContainsUniqueAddresses()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            string emailAddress = "john.doe@ssd.com";
            string password = "JohnDoe123!";
            List<Address> addresses = new()
            {
                new Address("Poland", "Warsaw", "00-000", "Złota", "1"),
                new Address("Poland", "Warsaw", "00-000", "Złota", "1")
            };

            // Act
            var user = new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, UserRole.User, new HashSet<Address>(addresses));

            // Assert
            user.Addresses.Should().Equal(addresses.Distinct());
        }

        [Fact]
        public void NewUser_ForValidData_ShouldCreateANewUser()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            string emailAddress = "john.doe@ssd.com";
            string password = "JohnDoe123!";
            List<Address> addresses = new()
            {
                new Address("Poland", "Warsaw", "00-000", "Złota", "1"),
                new Address("Poland", "Warsaw", "00-000", "Złota", "2")
            };

            // Act
            var user = new User(Guid.NewGuid(), new Fullname(firstName,
                lastName), emailAddress, password, UserRole.User, new HashSet<Address>(addresses));

            // Assert
            user.Addresses.Should().Equal(addresses);
            user.Fullname.Firstname.Should().Be(firstName);
            user.Fullname.Lastname.Should().Be(lastName);
            user.Email.EmailAddress.Should().Be(emailAddress);
            user.Password.VerifyPassword(password).Should().BeTrue();
            user.Status.Value.Should().Be(AccountStatus.Inactive);
            user.RoleOfUser.UserRole.Should().Be(UserRole.User);
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
