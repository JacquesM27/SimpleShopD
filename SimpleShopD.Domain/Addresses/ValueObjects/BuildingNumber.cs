using SimpleShopD.Domain.Addresses.Exceptions;

namespace SimpleShopD.Domain.Addresses.ValueObjects
{
    public sealed record BuildingNumber
    {
        public string Value { get; }

        public BuildingNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyAddressParameterException("Building number cannot be empty");
            if (value.Length is < 1 or > 100)
                throw new InvalidAddressParameterLengthException("BuildingNumber length must be longer than 1 and shorter than 100");

            Value = value;
        }

        public static implicit operator BuildingNumber(string value) => new(value);
        public static implicit operator string(BuildingNumber value) => value.Value;
    }
}
