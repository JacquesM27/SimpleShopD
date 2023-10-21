using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Addresses.Exceptions
{
    public class EmptyAddressParameterException : CustomException
    {
        public EmptyAddressParameterException(string message) : base(message) { }
    }
}
