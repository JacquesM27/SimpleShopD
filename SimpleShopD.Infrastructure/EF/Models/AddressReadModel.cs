namespace SimpleShopD.Infrastructure.EF.Models
{
    internal sealed class AddressReadModel
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }

        public UserReadModel? User { get; set; }
        public OrderReadModel? Order { get; set; }
    }
}
