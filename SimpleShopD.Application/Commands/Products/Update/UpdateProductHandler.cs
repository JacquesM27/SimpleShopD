using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Products.Update
{
    internal sealed class UpdateProductHandler : ICommandHandler<UpdateProduct>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
            => _productRepository = productRepository;

        public async Task HandleAsync(UpdateProduct command)
        {
            if (await _productRepository.ExistsByTitleAsync(command.Title))
                throw new ProductAlreadyExistException(command.Title);

            var product = await _productRepository.GetAsync(command.Id)
                ?? throw new ProductDoesNotExistException(command.Id.ToString());

            product.SetProperties(command.Title, command.Description, command.ProductType, command.Price);
            await _productRepository.UpdateAsync(product);
        }
    }
}
