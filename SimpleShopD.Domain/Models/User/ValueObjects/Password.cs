using System.Security.Cryptography;
using System.Text;

namespace SimpleShopD.Domain.Models.User.ValueObjects
{
    internal readonly record struct Password
    {
        public byte[] Hash { get; }
        public byte[] Salt { get; }

        public Password(string password)
        {
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
    }
}
