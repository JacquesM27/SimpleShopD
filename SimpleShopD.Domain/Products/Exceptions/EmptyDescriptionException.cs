namespace SimpleShopD.Domain.Products.Exceptions
{
    public class EmptyDescriptionException : Exception
    {
        public EmptyDescriptionException(string message) : base(message) { }
    }
}
