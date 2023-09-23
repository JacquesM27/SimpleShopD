using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Products.Exceptions
{
    public class EmptyTitleExeption : CustomException
    {
        public EmptyTitleExeption(string message) : base(message) { }
    }
}
