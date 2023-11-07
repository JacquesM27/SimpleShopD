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
            var product = await _productRepository.GetAsync(command.ProductId)
                ?? throw new ProductDoesNotExistException(command.ProductId.ToString());

            if (await _orderRepository.ProductExistsOnSomeOrder(command.ProductId))
                throw new ProductExistsOnSomeOrderException(command.ProductId.ToString());

            await _productRepository.DeleteAsync(product);
        }
    }
}
