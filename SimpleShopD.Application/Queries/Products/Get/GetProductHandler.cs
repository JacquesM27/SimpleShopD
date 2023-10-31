using SimpleShopD.Application.Dto;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Queries;

namespace SimpleShopD.Application.Queries.Products.Get;

internal sealed class GetProductHandler : IQueryHandler<GetProduct, ProductDto>
{
    private readonly IProductRepository _productRepository;

    public GetProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> HandleAsync(GetProduct query)
    {
        var product = await _productRepository.GetAsync(query.ProductId)
                      ?? throw new ProductDoesNotExistException(query.ProductId.ToString());

        return new ProductDto(product.Id, product.Title, product.Description,
            product.TypeOfProduct.ProductType.ToString(), product.Price);
    }
}