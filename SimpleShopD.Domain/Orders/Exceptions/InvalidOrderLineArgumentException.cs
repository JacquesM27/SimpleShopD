using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Orders.Exceptions
{
    public class InvalidOrderLineArgumentException : CustomException
    {
        public InvalidOrderLineArgumentException(string message) : base(message) { }
    }
}
