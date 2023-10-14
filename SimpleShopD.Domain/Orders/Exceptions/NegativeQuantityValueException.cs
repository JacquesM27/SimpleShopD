using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Orders.Exceptions
{
    public sealed class NegativeQuantityValueException : CustomException
    {
        public NegativeQuantityValueException(string message) : base(message)
        {
        }
    }
}
