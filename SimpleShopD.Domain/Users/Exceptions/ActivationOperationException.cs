using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Users.Exceptions
{
    public class ActivationOperationException : CustomException
    {
        public ActivationOperationException(string message) : base(message) { }
    }
}
