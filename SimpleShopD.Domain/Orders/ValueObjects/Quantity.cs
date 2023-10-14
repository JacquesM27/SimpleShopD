using SimpleShopD.Domain.Orders.Exceptions;

namespace SimpleShopD.Domain.Orders.ValueObjects
{
    public sealed record Quantity
    {
        public decimal Value { get; }

        public Quantity(decimal value)
        {
            if (value < 0)
                throw new NegativeQuantityValueException(value.ToString());

            Value = value;
        }

        public static implicit operator decimal(Quantity value) => value.Value; 
        public static implicit operator Quantity(decimal value) => new (value); 
    }
}
