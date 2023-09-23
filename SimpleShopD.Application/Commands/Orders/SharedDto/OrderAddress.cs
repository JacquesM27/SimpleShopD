namespace SimpleShopD.Application.Commands.Orders.SharedDto
{

    public sealed record OrderAddress(string Country, string City, string ZipCode, string Street, string BuildingNumber);
}
