using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShopD.Application.Commands.Orders.Return
{
    internal sealed class ReturnOrderHandler : ICommandHandler<ReturnOrder>
    {
        private readonly IOrderRepository _orderRepository;

        public ReturnOrderHandler(IOrderRepository orderRepository) 
            => _orderRepository = orderRepository;

        public async Task HandleAsync(ReturnOrder command)
        {
            var order = await _orderRepository.GetAsync(command.OrderId)
               ?? throw new OrderDoesNotExistException(command.OrderId.ToString());

            order.ReturnOrder();
            await _orderRepository.UpdateAsync(order);
        }
    }
}
