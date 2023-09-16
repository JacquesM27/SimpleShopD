using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Orders.Exceptions
{
    public sealed class ChangeAddressException : CustomException
    {
        public ChangeAddressException(string message) : base(message) { }
    }
}
