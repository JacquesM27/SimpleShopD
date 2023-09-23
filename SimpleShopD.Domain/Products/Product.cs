using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Products.ValueObjects;
using SimpleShopD.Domain.Shared.Base;

namespace SimpleShopD.Domain.Products
{
    public sealed class Product<T> : Entity<T> where T : notnull
    {
        public Title Title { get; private set; }
        public Description Description { get; private set; }
        public TypeOfProduct TypeOfProduct { get; private set; }
        public Price Price { get; private set; }

        public Product(T id, Title title, Description description, TypeOfProduct typeOfProduct, Price price) : base(id)
        {
            Title = title;
            Description = description;
            TypeOfProduct = typeOfProduct;
            Price = price;
        }

        public void SetProperties(Title title, Description description, TypeOfProduct typeOfProduct, Price price)
        {
            Title = title;
            Description = description;
            TypeOfProduct = typeOfProduct;
            Price = price;
        }
    }
}
