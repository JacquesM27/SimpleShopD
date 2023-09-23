using SimpleShopD.Application.Commands.Orders.SharedDto;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.AddOrderLine
{
    public sealed record AddOrderLine(Guid OrderId, OrderLine OrderLine) : ICommand;
}
