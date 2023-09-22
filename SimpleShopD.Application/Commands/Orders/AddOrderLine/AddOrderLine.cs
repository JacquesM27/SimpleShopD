using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.AddOrderLine
{
    public sealed record AddOrderLine(Guid OrderId, Guid ProductId, decimal Quantity, decimal Price) : ICommand;
}
