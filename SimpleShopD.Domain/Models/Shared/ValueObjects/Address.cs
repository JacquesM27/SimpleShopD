namespace SimpleShopD.Domain.Models.Shared.ValueObjects
{
    internal readonly record struct Address
    {
        public string Country { get; }
        public string City { get; }
        public string ZipCode { get; }
        public string Street { get; }
        public string BuildingNumber { get; }

        public Address(string country, string city, string zipCode, string street, string buildingNumber, bool mainAddress)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentNullException(nameof(country));
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException(nameof(city));
            if (string.IsNullOrWhiteSpace(zipCode))
                throw new ArgumentNullException(nameof(zipCode));
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentNullException(nameof(street));
            if (string.IsNullOrWhiteSpace(buildingNumber))
                throw new ArgumentNullException(nameof(buildingNumber));
            if (country.Length is < 2 or > 100)
                throw new ArgumentException("Country length must be longer than 2 and shorter than 100");
            if (city.Length is < 2 or > 100)
                throw new ArgumentException("City length must be longer than 2 and shorter than 100");
            if (zipCode.Length is < 2 or > 100)
                throw new ArgumentException("ZipCode length must be longer than 2 and shorter than 100");
            if (street.Length is < 2 or > 100)
                throw new ArgumentException("Street length must be longer than 2 and shorter than 100");
            if (buildingNumber.Length is < 1 or > 100)
                throw new ArgumentException("BuildingNumber length must be longer than 1 and shorter than 100");

            Country = country;
            City = city;
            ZipCode = zipCode;
            Street = street;
            BuildingNumber = buildingNumber;
        }
    }
}
