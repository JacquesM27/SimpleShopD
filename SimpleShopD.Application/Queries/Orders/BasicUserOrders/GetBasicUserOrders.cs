using SimpleShopD.Application.Dto;
using SimpleShopD.Shared.Abstractions.Queries;

namespace SimpleShopD.Application.Queries.Orders.GetBasic
{
    public sealed record GetBasicUserOrders() : IQuery<IEnumerable<OrderBasicDto>>;
}
