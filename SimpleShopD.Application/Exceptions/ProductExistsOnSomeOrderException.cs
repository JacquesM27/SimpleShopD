using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Application.Exceptions
{
    internal sealed class ProductExistsOnSomeOrderException : CustomException
    {
        public ProductExistsOnSomeOrderException(string message) : base(message)
        {
        }
    }
}
