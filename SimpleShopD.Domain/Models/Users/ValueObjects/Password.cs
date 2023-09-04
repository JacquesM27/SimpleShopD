using System.Security.Cryptography;
using System.Text;

namespace SimpleShopD.Domain.Models.Users.ValueObjects
{
    internal readonly record struct Password
    {
        private readonly int _minumumPasswordLenght = 8;

        public byte[] Hash { get; }
        public byte[] Salt { get; }

        public Password(string password)
        {
            if (!HasPasswordValidPolicy(password))
                throw new ArgumentException($"The password does not meet security requirements. It should have a minimum of {_minumumPasswordLenght} characters, contain lowercase and uppercase letters, numbers and special characters");

            using var hmac = new HMACSHA512();
            Salt = hmac.Key;
            Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyPassword(string password)
        {
            using var hmac = new HMACSHA512(Salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var result = computedHash.SequenceEqual(Hash);
            return result;
        }

        private bool HasPasswordValidPolicy(string password)
        {
            if (password.Length < _minumumPasswordLenght)
                return false;

            return password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit) &&
                   password.Any(IsSpecialCharacter);
        }

        private bool IsSpecialCharacter(char c)
        {
            return "!@#$%^&*()_+-=[]{};':\"\\|,.<>?".Contains(c);
        }

        public static implicit operator Password(string password) => new(password);
    }
}
