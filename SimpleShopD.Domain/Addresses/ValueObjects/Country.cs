using SimpleShopD.Domain.Addresses.Exceptions;

namespace SimpleShopD.Domain.Addresses.ValueObjects
{
    public sealed record Country
    {
        public string Value { get; }

        public Country(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyAddressParameterException("Country cannot be empty");
            if (value.Length is < 2 or > 100)
                throw new InvalidAddressParameterLengthException("Country length must be longer than 2 and shorter than 100");

            Value = value;
        }

        public static implicit operator Country(string value) => new(value);
        public static implicit operator string(Country country) => country.Value;
    }
}
