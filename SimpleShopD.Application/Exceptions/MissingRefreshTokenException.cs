using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Application.Exceptions
{
    public sealed class MissingRefreshTokenException : CustomException
    {
        public MissingRefreshTokenException(string message) : base(message)
        {
        }
    }
}
