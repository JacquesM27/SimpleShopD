using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.Pay
{
    internal sealed class PayOrderHandler : ICommandHandler<PayOrder>
    {
        private readonly IOrderRepository _orderRepository;

        public PayOrderHandler(IOrderRepository orderRepository)
            => _orderRepository = orderRepository;

        public async Task HandleAsync(PayOrder command)
        {
            var order = await _orderRepository.GetAsync(command.OrderId)
               ?? throw new OrderDoesNotExistException(command.OrderId.ToString());

            order.PayOrder(command.Amount);
            await _orderRepository.UpdateAsync(order);
        }
    }
}
