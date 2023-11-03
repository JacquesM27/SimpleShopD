using SimpleShopD.Application.Dto;
using SimpleShopD.Application.Queries.Orders.GetBasic;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Queries;

namespace SimpleShopD.Application.Queries.Orders.BasicUserOrders
{
    internal sealed class GetBasicUserOrdersHandler : IQueryHandler<GetBasicUserOrders, IEnumerable<OrderBasicDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetBasicUserOrdersHandler(IOrderRepository orderRepository) 
            => _orderRepository = orderRepository;

        public async Task<IEnumerable<OrderBasicDto>> HandleAsync(GetBasicUserOrders query) 
            => (await _orderRepository.GetByUserAsync(query.UserId))
            .Select(x => new OrderBasicDto(x.Id, x.CreationDate, x.Status.Value.ToString(), x.GetTotal()));

    }
}
