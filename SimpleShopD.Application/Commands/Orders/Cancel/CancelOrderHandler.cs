using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
