using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Users.Exceptions
{
    public sealed class RefreshTokenOperationException : CustomException
    {
        public RefreshTokenOperationException(string message) : base(message)
        {
        }
    }
}
