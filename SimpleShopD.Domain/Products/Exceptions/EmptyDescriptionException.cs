using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Products.Exceptions
{
    public class EmptyDescriptionException : CustomException
    {
        public EmptyDescriptionException(string message) : base(message) { }
    }
}
