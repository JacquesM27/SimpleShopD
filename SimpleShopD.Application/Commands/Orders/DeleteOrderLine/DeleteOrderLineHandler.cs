using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.DeleteOrderLine
{
    internal sealed class DeleteOrderLineHandler : ICommandHandler<DeleteOrderLine>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderLineHandler(IOrderRepository orderRepository)
            => _orderRepository = orderRepository;

        public async Task HandleAsync(DeleteOrderLine command)
        {
            var order = await _orderRepository.GetAsync(command.OrderId)
                ?? throw new OrderDoesNotExistException(command.OrderId.ToString());

            order.RemoveOrderLine(command.No);
        }
    }
}
