namespace SimpleShopD.Domain.Orders.Exceptions
{
    public sealed class ChangeStatusOfOrderException : Exception
    {
        public ChangeStatusOfOrderException(string message) : base(message) { }
    }
}
