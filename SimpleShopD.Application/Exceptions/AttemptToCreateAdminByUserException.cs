using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Application.Exceptions
{
    public sealed class AttemptToCreateAdminByUserException : CustomException
    {
        public AttemptToCreateAdminByUserException(string message) : base(message)
        {
        }
    }
}
