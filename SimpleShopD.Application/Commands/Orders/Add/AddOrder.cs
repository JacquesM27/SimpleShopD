using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.Add
{
    public sealed record AddOrder(Guid UserId, OrderAddress OrderAddress, string FirstName, string LastName, IEnumerable<OrderLine> OrderLines) : ICommand;
    public sealed record OrderAddress(string Country, string City, string ZipCode, string Street, string BuildingNumber);
    public sealed record OrderLine(Guid ProductId, decimal Quantity, decimal SalePrice);
}