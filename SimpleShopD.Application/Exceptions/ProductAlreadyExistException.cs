using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Application.Exceptions
{
    public sealed class ProductAlreadyExistException : CustomException
    {
        public ProductAlreadyExistException(string message) : base(message) { }
    }
}
