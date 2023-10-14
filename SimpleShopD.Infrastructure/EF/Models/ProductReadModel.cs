using SimpleShopD.Domain.Enum;

namespace SimpleShopD.Infrastructure.EF.Models
{
    internal sealed class ProductReadModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ProductType TypeOfProduct { get; set; }
        public decimal Price { get; set; }
    }
}
