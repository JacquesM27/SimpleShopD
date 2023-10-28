using SimpleShopD.Application.Dto;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.AddOrderLine
{
    public sealed record AddOrderLine(Guid OrderId, OrderLine OrderLine) : ICommand;
}
