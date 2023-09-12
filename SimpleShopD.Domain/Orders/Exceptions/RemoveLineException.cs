namespace SimpleShopD.Domain.Orders.Exceptions
{
    public sealed class RemoveLineException : Exception
    {
        public RemoveLineException(string message) : base(message) { }
    }
}
