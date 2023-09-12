using SimpleShopD.Doman.Products.ValueObjects;
using SimpleShopD.Doman.Shared.Base;

namespace SimpleShopD.Doman.Products
{
    public sealed class Product<T> : Entity<T>  where T : notnull
    {
        public Title Title { get; }
        public Description Description { get; }
        public TypeOfProduct TypeOfProduct { get; }
        public Price Price { get; }

        public Product(T id, Title title, Description description, TypeOfProduct typeOfProduct, Price price) : base(id)
        {
            Title = title;
            Description = description;
            TypeOfProduct = typeOfProduct;
            Price = price;
        }
    }
}
