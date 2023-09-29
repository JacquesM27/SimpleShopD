using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Application.Exceptions
{
    public sealed class CannotCreateUserPolicyException : CustomException
    {
        public CannotCreateUserPolicyException(string message) : base(message)
        {
        }
    }
}
