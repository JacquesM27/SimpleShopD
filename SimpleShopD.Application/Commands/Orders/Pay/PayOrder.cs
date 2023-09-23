using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.Pay
{
    public sealed record PayOrder(Guid OrderId, decimal Amount) : ICommand;
}
