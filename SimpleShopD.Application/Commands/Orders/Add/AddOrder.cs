using SimpleShopD.Application.Commands.SharedDto;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.Add
{
    public sealed record AddOrder(Guid UserId, Guid OrderAddress, string FirstName, string LastName, IEnumerable<OrderLine> OrderLines) : ICommand;
}