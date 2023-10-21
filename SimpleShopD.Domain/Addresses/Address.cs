using SimpleShopD.Domain.Addresses.ValueObjects;
using SimpleShopD.Domain.Shared.Base;

namespace SimpleShopD.Domain.Addresses
{
    public class Address : Entity<Guid>
    {
        public Country Country { get; }
        public City City { get; }
        public ZipCode ZipCode { get; }
        public Street Street { get; }
        public BuildingNumber BuildingNumber { get; }

        internal Address(Guid id, string country, string city, string zipCode, string street, string buildingNumber)
            : base(id)
        {
            Country = country;
            City = city;
            ZipCode = zipCode;
            Street = street;
            BuildingNumber = buildingNumber;
        }

        private Address() : base(Guid.NewGuid()) { }
    }
}
