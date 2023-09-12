namespace SimpleShopD.Domain.Orders.Exceptions
{
    public sealed class PayOrderException : Exception
    {
        public PayOrderException(string message) : base(message) { }
    }
}
