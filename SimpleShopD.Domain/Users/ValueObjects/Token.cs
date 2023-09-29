using SimpleShopD.Domain.Enum;
using System.Security.Cryptography;

namespace SimpleShopD.Domain.Users.ValueObjects
{
    public sealed record Token
    {
        public string Value { get; }
        public DateTime ExpirationDate { get; }
        public bool IsExpired => DateTime.UtcNow > ExpirationDate;

        public Token(TokenType tokenType)
        {
            Value = GenerateToken();
            ExpirationDate = tokenType switch
            {
                TokenType.Activation => DateTime.UtcNow.AddDays(1),
                TokenType.ResetPassword => DateTime.UtcNow.AddMinutes(30),
                TokenType.Refresh => DateTime.UtcNow.AddMinutes(30),
                _ => throw new ArgumentException("Value not implemented."),
            };
        }

        private static string GenerateToken()
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] tokenData = new byte[64];
            rng.GetBytes(tokenData);
            return Convert.ToBase64String(tokenData);
        }

        public static implicit operator Token(TokenType tokenType) => new(tokenType);
    }
}
