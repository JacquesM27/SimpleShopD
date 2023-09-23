using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.AddOrderLine
{
    internal sealed class AddOrderLineHandler : ICommandHandler<AddOrderLine>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public AddOrderLineHandler(IOrderRepository orderRepository, 
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task HandleAsync(AddOrderLine command)
        {
            var order = await _orderRepository.GetAsync(command.OrderId)
                ?? throw new OrderDoesNotExistException(command.OrderId.ToString());

            if(await _productRepository.DoesExist(command.OrderLine.ProductId))
                throw new ProductDoesNotExistException(command.OrderLine.ProductId.ToString());

            order.AddOrderLine(Guid.NewGuid(), command.OrderLine.ProductId, command.OrderLine.Quantity, command.OrderLine.Price);
        }
    }
}
