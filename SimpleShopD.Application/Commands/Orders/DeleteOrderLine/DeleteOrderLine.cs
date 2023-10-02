using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.DeleteOrderLine
{
    public sealed record DeleteOrderLine(Guid OrderId, Guid LineId) : ICommand;
}
