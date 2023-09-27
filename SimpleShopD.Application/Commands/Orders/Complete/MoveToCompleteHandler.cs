using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.Complete
{
    internal sealed class MoveToCompleteHandler : ICommandHandler<MoveToComplete>
    {
        private readonly IOrderRepository _orderRepository;

        public MoveToCompleteHandler(IOrderRepository orderRepository)
            => _orderRepository = orderRepository;

        public async Task HandleAsync(MoveToComplete command)
        {
            var order = await _orderRepository.GetAsync(command.OrderId)
               ?? throw new OrderDoesNotExistException(command.OrderId.ToString());

            order.MoveToComplete();
            await _orderRepository.UpdateAsync(order);
        }
    }
}
