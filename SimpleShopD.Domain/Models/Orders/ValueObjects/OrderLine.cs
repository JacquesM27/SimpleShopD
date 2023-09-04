namespace SimpleShopD.Domain.Models.Orders.ValueObjects
{
    internal readonly record struct OrderLine<T> where T : notnull
    {
        public int No { get; }
        public decimal SalePrice { get; }
        public decimal Quantity { get; }
        public T ProductId { get; }

        // TODO: Add constructor
    }
}
