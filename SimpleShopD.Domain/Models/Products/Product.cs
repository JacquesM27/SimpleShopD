using SimpleShopD.Domain.Models.Products.ValueObjects;
using SimpleShopD.Domain.Models.Shared.Base;

namespace SimpleShopD.Domain.Models.Products
{
    internal sealed class Product<T> : Entity<T>  where T : notnull
    {
        public Title Title { get; }
        public Description Description { get; }
        public TypeProduct TypeProduct { get; }
        public Price Price { get; }

        public Product(T id, Title title, Description description, TypeProduct typeProduct, Price price) : base(id)
        {
            Title = title;
            Description = description;
            TypeProduct = typeProduct;
            Price = price;
        }
    }
}
