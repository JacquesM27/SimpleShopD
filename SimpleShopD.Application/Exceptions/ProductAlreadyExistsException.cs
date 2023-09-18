using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Application.Exceptions
{
    public sealed class ProductAlreadyExistsException : CustomException
    {
        public ProductAlreadyExistsException(string message) : base(message) { }
    }
}
