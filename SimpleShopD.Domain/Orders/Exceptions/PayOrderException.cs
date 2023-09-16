using SimpleShopD.Shared.Abstractions.Exceptions;
namespace SimpleShopD.Domain.Orders.Exceptions
{
    public sealed class PayOrderException : CustomException
    {
        public PayOrderException(string message) : base(message) { }
    }
}
