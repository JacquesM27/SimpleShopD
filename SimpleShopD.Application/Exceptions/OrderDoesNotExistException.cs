using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Application.Exceptions
{
    public sealed class OrderDoesNotExistException : CustomException
    {
        public OrderDoesNotExistException(string message) : base(message)
        {
        }
    }
}
