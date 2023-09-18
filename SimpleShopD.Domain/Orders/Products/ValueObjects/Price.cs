using SimpleShopD.Domain.Orders.Products.Exceptions;

namespace SimpleShopD.Domain.Orders.Products.ValueObjects
{
    public sealed record Price
    {
        public decimal Value { get; }

        public Price(decimal value)
        {
            if (value < 0)
                throw new IncorrectPriceValueException("Price cannot be negative.");

            Value = value;
        }

        public static implicit operator decimal(Price price) => price.Value;

        public static implicit operator Price(decimal value) => new(value);
    }
}
