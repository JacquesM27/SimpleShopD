namespace SimpleShopD.Application.Commands.SharedDto
{
    internal static class AddressExtension
    {
        internal static Domain.Shared.ValueObjects.Address MapToDomainAddress(this Address address)
         => new(
                address.Country,
                address.City,
                address.ZipCode,
                address.Street,
                address.BuildingNumber);
    }
}
