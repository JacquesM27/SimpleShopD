using SimpleShopD.Shared.Abstractions.Exceptions;
namespace SimpleShopD.Domain.Addresses.Exceptions
{
    public class InvalidAddressParameterLengthException : CustomException
    {
        public InvalidAddressParameterLengthException(string message) : base(message) { }
    }
}
