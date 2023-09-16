using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Products.Exceptions
{
    public class IncorrectPriceValueException : CustomException
    {
        public IncorrectPriceValueException(string message) : base(message) { }
    }
}
