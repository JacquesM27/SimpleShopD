using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Application.Exceptions
{
    public sealed class InvalidRefreshTokenException : CustomException
    {
        public InvalidRefreshTokenException(string message) : base(message)
        {
        }
    }
}
