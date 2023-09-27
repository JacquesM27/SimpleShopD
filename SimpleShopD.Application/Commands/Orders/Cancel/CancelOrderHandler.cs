using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.Cancel
{
    internal sealed class CancelOrderHandler : ICommandHandler<CancelOrder>
    {
        private readonly IOrderRepository _orderRepository;

        public CancelOrderHandler(IOrderRepository orderRepository)
            => _orderRepository = orderRepository;

        public async Task HandleAsync(CancelOrder command)
        {
            var order = await _orderRepository.GetAsync(command.OrderId)
               ?? throw new OrderDoesNotExistException(command.OrderId.ToString());

            order.CancelOrder();
            await _orderRepository.UpdateAsync(order);
        }
    }
}
