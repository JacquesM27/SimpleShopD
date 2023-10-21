using SimpleShopD.Domain.Addresses.Exceptions;

namespace SimpleShopD.Domain.Addresses.ValueObjects
{
    public sealed record ZipCode
    {
        public string Value { get; }

        public ZipCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyAddressParameterException("Zipcode cannot be empty");
            if (value.Length is < 2 or > 100)
                throw new InvalidAddressParameterLengthException("ZipCode length must be longer than 2 and shorter than 100");

            Value = value;
        }

        public static implicit operator ZipCode(string value) => new(value);
        public static implicit operator string(ZipCode value) => value.Value;
    }
}
