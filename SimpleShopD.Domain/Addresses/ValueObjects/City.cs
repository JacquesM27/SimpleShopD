using SimpleShopD.Domain.Addresses.Exceptions;

namespace SimpleShopD.Domain.Addresses.ValueObjects
{
    public sealed record City
    {
        public string Value { get; }

        public City(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyAddressParameterException("City cannot be empty");
            if (value.Length is < 2 or > 200)
                throw new InvalidAddressParameterLengthException("City length must be longer than 2 and shorter than 200");

            Value = value;
        }

        public static implicit operator City(string value) => new(value);
        public static implicit operator string(City city) => city.Value;
    }
}
