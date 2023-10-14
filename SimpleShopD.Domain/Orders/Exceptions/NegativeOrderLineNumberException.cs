using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Orders.Exceptions
{
    public class NegativeOrderLineNumberException : CustomException
    {
        public NegativeOrderLineNumberException(string message) : base(message) { }
    }
}
