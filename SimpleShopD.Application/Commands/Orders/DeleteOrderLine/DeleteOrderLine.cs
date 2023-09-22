using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.DeleteOrderLine
{
    public sealed record DeleteOrderLine(Guid OrderId, int No) : ICommand;
}
