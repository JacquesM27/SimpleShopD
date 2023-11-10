using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Users.Exceptions
{
    public sealed class InvalidTokenLengthException : CustomException
    {
        public InvalidTokenLengthException(string message) : base(message)
        {
        }
    }
}
