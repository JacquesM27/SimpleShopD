using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Orders.Products.Exceptions
{
    public class EmptyDescriptionException : CustomException
    {
        public EmptyDescriptionException(string message) : base(message) { }
    }
}
