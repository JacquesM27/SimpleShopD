using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Application.Exceptions
{
    internal class AddressDoesNotExistException : CustomException
    {
        public AddressDoesNotExistException(string message) : base(message)
        {
        }
    }
}
