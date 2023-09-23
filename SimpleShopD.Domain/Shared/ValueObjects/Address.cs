using SimpleShopD.Domain.Shared.Exceptions;

namespace SimpleShopD.Domain.Shared.ValueObjects
{
    public sealed record Address
    {
        public string Country { get; }
        public string City { get; }
        public string ZipCode { get; }
        public string Street { get; }
        public string BuildingNumber { get; }

        public Address(string country, string city, string zipCode, string street, string buildingNumber)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new EmptyAddressParameterException("Country cannot be empty");
            if (string.IsNullOrWhiteSpace(city))
                throw new EmptyAddressParameterException("City cannot be empty");
            if (string.IsNullOrWhiteSpace(zipCode))
                throw new EmptyAddressParameterException("Zipcode cannot be empty");
            if (string.IsNullOrWhiteSpace(street))
                throw new EmptyAddressParameterException("Street cannot be empty");
            if (string.IsNullOrWhiteSpace(buildingNumber))
                throw new EmptyAddressParameterException("Building number cannot be empty");
            if (country.Length is < 2 or > 100)
                throw new InvalidAddressParameterLengthException("Country length must be longer than 2 and shorter than 100");
            if (city.Length is < 2 or > 100)
                throw new InvalidAddressParameterLengthException("City length must be longer than 2 and shorter than 100");
            if (zipCode.Length is < 2 or > 100)
                throw new InvalidAddressParameterLengthException("ZipCode length must be longer than 2 and shorter than 100");
            if (street.Length is < 2 or > 100)
                throw new InvalidAddressParameterLengthException("Street length must be longer than 2 and shorter than 100");
            if (buildingNumber.Length is < 1 or > 100)
                throw new InvalidAddressParameterLengthException("BuildingNumber length must be longer than 1 and shorter than 100");

            Country = country;
            City = city;
            ZipCode = zipCode;
            Street = street;
            BuildingNumber = buildingNumber;
        }
    }
}
