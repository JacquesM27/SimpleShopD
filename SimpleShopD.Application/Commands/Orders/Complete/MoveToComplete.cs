using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.Complete
{
    public sealed record MoveToComplete(Guid OrderId) : ICommand;
}
