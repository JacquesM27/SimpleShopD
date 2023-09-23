using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.Pending
{
    public sealed record MoveToPending(Guid OrderId) : ICommand;
}
