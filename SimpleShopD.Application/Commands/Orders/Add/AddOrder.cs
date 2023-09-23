using SimpleShopD.Application.Commands.Orders.SharedDto;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.Add
{
    public sealed record AddOrder(Guid UserId, OrderAddress OrderAddress, string FirstName, string LastName, IEnumerable<OrderLine> OrderLines) : ICommand;
}