using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShopD.Application.Commands.Orders.Pending
{
    internal sealed class MoveToPendingHandler : ICommandHandler<MoveToPending>
    {
        private readonly IOrderRepository _orderRepository;

        public MoveToPendingHandler(IOrderRepository orderRepository) 
            => _orderRepository = orderRepository;

        public async Task HandleAsync(MoveToPending command)
        {
            var order = await _orderRepository.GetAsync(command.OrderId)
               ?? throw new OrderDoesNotExistException(command.OrderId.ToString());

            order.MoveToPending();
            await _orderRepository.UpdateAsync(order);
        }
    }
}
