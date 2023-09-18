using SimpleShopD.Domain.Orders.Products.Exceptions;

namespace SimpleShopD.Domain.Orders.Products.ValueObjects
{
    public sealed record Description
    {
        public string Value { get; }

        public Description(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyDescriptionException("Description cannot be empty");

            Value = value;
        }

        public static implicit operator Description(string value) => new(value);

        public static implicit operator string(Description description) => description.Value;
    }
}
