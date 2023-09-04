﻿namespace SimpleShopD.Domain.Models.Products.ValueObjects
{
    internal readonly record struct Price
    {
        public decimal Value { get; }

        public Price(decimal value)
        {
            if(value < 0)
                throw new ArgumentException("Price cannot be negative.");

            Value = value;
        }

        public static implicit operator decimal(Price price) => price.Value;

        public static implicit operator Price(decimal value) => new(value);
    }
}
