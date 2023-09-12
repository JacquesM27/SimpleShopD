namespace SimpleShopD.Domain.Orders.Exceptions
{
    public class InvalidOrderLineArgumentException : Exception
    {
        public InvalidOrderLineArgumentException(string message) : base(message) { }
    }
}
