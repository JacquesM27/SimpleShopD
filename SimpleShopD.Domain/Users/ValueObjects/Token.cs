using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Users.Exceptions;

namespace SimpleShopD.Domain.Users.ValueObjects
{
    public sealed record Token
    {
        public string Value { get; }
        public DateTime ExpirationDate { get; }
        public bool IsExpired => DateTime.UtcNow > ExpirationDate;

        public Token(TokenType tokenType, string value)
        {
            if (string.IsNullOrEmpty(value)) 
                throw new InvalidTokenLengthException(nameof(value));

            Value = value;
            ExpirationDate = tokenType switch
            {
                TokenType.Activation => DateTime.UtcNow.AddDays(1),
                TokenType.ResetPassword => DateTime.UtcNow.AddMinutes(30),
                TokenType.Refresh => DateTime.UtcNow.AddMinutes(30),
                _ => throw new ArgumentException("Value not implemented."),
            };
        }

        private Token() { }
    }
}
