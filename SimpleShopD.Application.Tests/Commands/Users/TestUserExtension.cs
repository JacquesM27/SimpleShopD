using SimpleShopD.Domain.Users;
using SimpleShopD.Domain.Users.ValueObjects;
using System.Security.Cryptography;

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
                lastName), emailAddress, password, role, GenerateRandomToken());
        }

        public static string GenerateRandomToken()
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] tokenData = new byte[64];
            rng.GetBytes(tokenData);
            return Convert.ToBase64String(tokenData);
        }
    }
}
