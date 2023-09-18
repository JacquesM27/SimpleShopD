using SimpleShopD.Shared.Abstractions.Exceptions;

namespace SimpleShopD.Domain.Orders.Products.Exceptions
{
    public class EmptyTitleExeption : CustomException
    {
        public EmptyTitleExeption(string message) : base(message) { }
    }
}
