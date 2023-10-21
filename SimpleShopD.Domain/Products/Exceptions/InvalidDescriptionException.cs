using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Products.Exceptions
{
    public sealed class InvalidDescriptionException : CustomException
    {
        public InvalidDescriptionException(string message) : base(message)
        {
        }
    }
}
