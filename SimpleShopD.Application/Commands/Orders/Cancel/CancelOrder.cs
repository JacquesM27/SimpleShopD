using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.Cancel
{
    public sealed record CancelOrder(Guid OrderId) : ICommand;
}
