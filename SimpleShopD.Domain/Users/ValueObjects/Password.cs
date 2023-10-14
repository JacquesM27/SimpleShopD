﻿using SimpleShopD.Domain.Users.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace SimpleShopD.Domain.Users.ValueObjects
{
    public sealed record Password
    {
        private readonly int _minumumPasswordLength = 8;

        public byte[] Hash { get; }
        public byte[] Salt { get; }

        public Password(string password)
        {
            if (!HasPasswordValidPolicy(password))
                throw new PasswordPolicyException($"The password does not meet security requirements. It should have a minimum of {_minumumPasswordLength} characters, contain lowercase and uppercase letters, numbers and special characters");

            using var hmac = new HMACSHA512();
            Salt = hmac.Key;
            Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public Password(byte[] hash, byte[] salt) 
        {
            Hash = hash;
            Salt = salt;
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
            if (password.Length < _minumumPasswordLength)
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
