namespace SimpleShopD.Infrastructure.EF.Models
{
    internal sealed class OrderLineReadModel
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }

        public ProductReadModel Product { get; set; }
        public OrderReadModel Order { get; set; }
    }
}
