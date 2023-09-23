using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Products.Delete
{
    internal sealed class DeleteProductHandler : ICommandHandler<DeleteProduct>
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public DeleteProductHandler(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task HandleAsync(DeleteProduct command)
        {
            var product = await _productRepository.GetAsync(command.Id)
                ?? throw new ProductDoesNotExistException(command.Id.ToString());

            if(await _orderRepository.ProductExistsOnSomeOrder(command.Id))
                throw new ProductExistsOnSomeOrderException(command.Id.ToString());

            await _productRepository.DeleteAsync(product);
        }
    }
}
