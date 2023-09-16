using SimpleShopD.Shared.Abstractions.Exceptions;
namespace SimpleShopD.Domain.Orders.Exceptions
{
    public sealed class RemoveLineException : CustomException
    {
        public RemoveLineException(string message) : base(message) { }
    }
}
