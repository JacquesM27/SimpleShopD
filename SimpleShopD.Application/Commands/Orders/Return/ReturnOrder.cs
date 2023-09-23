using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.Return
{
    public sealed record ReturnOrder(Guid OrderId) : ICommand;
}
