using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Orders.Exceptions
{
    public sealed class ChangeStatusOfOrderException : CustomException
    {
        public ChangeStatusOfOrderException(string message) : base(message) { }
    }
}
