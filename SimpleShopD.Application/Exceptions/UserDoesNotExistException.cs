using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Application.Exceptions
{
    public sealed class UserDoesNotExistException : CustomException
    {
        public UserDoesNotExistException(string message) : base(message)
        {
        }
    }
}
