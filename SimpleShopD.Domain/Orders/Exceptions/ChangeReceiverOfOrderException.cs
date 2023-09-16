using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Orders.Exceptions
{
    public sealed class ChangeReceiverOfOrderException : CustomException
    {
        public ChangeReceiverOfOrderException(string message) : base(message) { }
    }
}
