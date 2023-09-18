using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Application.Exceptions
{
    public sealed class ProductDoesNotExistException : CustomException
    {
        public ProductDoesNotExistException(string message) : base(message) { }
    }
}
