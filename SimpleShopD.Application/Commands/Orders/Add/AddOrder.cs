using SimpleShopD.Application.Dto;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.Add
{
    public sealed record AddOrder(Guid OrderAddress, string FirstName, string LastName, IEnumerable<OrderLine> OrderLines) : ICommandTResult<Guid>;
}