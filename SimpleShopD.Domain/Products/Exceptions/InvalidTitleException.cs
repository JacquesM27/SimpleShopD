using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Products.Exceptions
{
    public sealed class InvalidTitleException : CustomException
    {
        public InvalidTitleException(string message) : base(message)
        {
        }
    }
}
