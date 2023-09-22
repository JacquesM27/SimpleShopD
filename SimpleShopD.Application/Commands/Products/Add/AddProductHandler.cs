using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Orders.Products;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Products.Add
{
    internal sealed class AddProductHandler : ICommandIdHandler<AddProduct, Guid>
    {
        private readonly IProductRepository _productRepository;

        public AddProductHandler(IProductRepository productRepository) 
            => _productRepository = productRepository;

        public async Task<Guid> HandleAsync(AddProduct command)
        {
            if(await _productRepository.ExistsByTitleAsync(command.Title))
                throw new ProductAlreadyExistException(command.Title);

            var product = new Product<Guid>(
                id: Guid.NewGuid(),
                title: command.Title,
                description: command.Description,
                typeOfProduct: command.ProductType,
                price: command.Price);

            var id = await _productRepository.AddAsync(product);
            return id;
        }
    }
}
