using SimpleShopD.Domain.Addresses.Exceptions;

namespace SimpleShopD.Domain.Addresses.ValueObjects
{
    public sealed record Street
    {
        public string Value { get; }

        public Street(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyAddressParameterException("Street cannot be empty");
            if (value.Length is < 2 or > 200)
                throw new InvalidAddressParameterLengthException("Street length must be longer than 2 and shorter than 200");

            Value = value;
        }

        public static implicit operator Street(string value) => new(value);
        public static implicit operator string(Street street) => street.Value;
    }
}
