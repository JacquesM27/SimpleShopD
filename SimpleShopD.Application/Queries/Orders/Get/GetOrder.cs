using SimpleShopD.Application.Dto;
using SimpleShopD.Shared.Abstractions.Queries;

namespace SimpleShopD.Application.Queries.Orders.Get
{
    public sealed record GetOrder(Guid OrderId) : IQuery<OrderDto>;
}
