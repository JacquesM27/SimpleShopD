using SimpleShopD.Application.Dto;
using SimpleShopD.Application.Queries.Orders.GetBasic;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Services;
using SimpleShopD.Shared.Abstractions.Queries;

namespace SimpleShopD.Application.Queries.Orders.BasicUserOrders
{
    internal sealed class GetBasicUserOrdersHandler : IQueryHandler<GetBasicUserOrders, IEnumerable<OrderBasicDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IContextAccessor _contextAccessor;

        public GetBasicUserOrdersHandler(IOrderRepository orderRepository, IContextAccessor contextAccessor)
        {
            _orderRepository = orderRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<IEnumerable<OrderBasicDto>> HandleAsync(GetBasicUserOrders query)
        {
            var userId = _contextAccessor.GetUserId();
            return (await _orderRepository.GetByUserAsync(userId))
            .Select(x => new OrderBasicDto(x.Id, x.CreationDate, x.Status.Value.ToString(), x.GetTotal()));
        }

    }
}
