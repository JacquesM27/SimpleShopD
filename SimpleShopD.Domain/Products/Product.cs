﻿using SimpleShopD.Domain.Products.ValueObjects;
using SimpleShopD.Domain.Shared.Base;
using SimpleShopD.Domain.Shared.ValueObjects;

namespace SimpleShopD.Domain.Products
{
    public sealed class Product : AggregateRoot<Guid>
    {
        public Title Title { get; private set; }
        public Description Description { get; private set; }
        public TypeOfProduct TypeOfProduct { get; private set; }
        public Price Price { get; private set; }

        public Product(Guid id, Title title, Description description, TypeOfProduct typeOfProduct, Price price) : base(id)
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
