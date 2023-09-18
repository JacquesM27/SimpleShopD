using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Products.Delete
{
    internal sealed class DeleteProductHandler : ICommandHandler<DeleteProduct>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandler(IProductRepository productRepository) 
            => _productRepository = productRepository;

        public async Task HandleAsync(DeleteProduct command)
        {
            var product = await _productRepository.GetAsync(command.Id)
                ?? throw new ProductDoesNotExistException(command.Id.ToString());

            await _productRepository.DeleteByIdAsync(product);
        }
    }
}
