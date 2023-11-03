using SimpleShopD.Domain.Users;
using SimpleShopD.Domain.Users.ValueObjects;

namespace SimpleShopD.Application.Tests.Commands.Users
{
    internal static class TestUserExtension
    {
        public static User CreateValidUser(Role role)
        {
            string firstName = "John";
            string lastName = "Doe";
            string emailAddress = "john.doe@ssd.com";
            string password = "JohnDoe123!";

            return new(Guid.NewGuid(), new Domain.Shared.ValueObjects.Fullname(firstName,
                lastName), emailAddress, password, role);
        }
    }
}
