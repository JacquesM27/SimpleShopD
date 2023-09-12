using SimpleShopD.Domain.Products.Exceptions;

namespace SimpleShopD.Doman.Products.ValueObjects
{
    public sealed record Price
    {
        public decimal Value { get; }

        public Price(decimal value)
        {
            if(value < 0)
                throw new IncorrectPriceValueException("Price cannot be negative.");

            Value = value;
        }

        public static implicit operator decimal(Price price) => price.Value;

        public static implicit operator Price(decimal value) => new(value);
    }
}
