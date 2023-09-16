using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Shared.Exceptions
{
    public class EmptyAddressParameterException : CustomException
    {
        public EmptyAddressParameterException(string message) : base(message) { }
    }
}
