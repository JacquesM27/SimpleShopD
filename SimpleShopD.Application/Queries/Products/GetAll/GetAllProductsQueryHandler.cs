using SimpleShopD.Application.Dto;
using SimpleShopD.Domain.Products;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Queries;

namespace SimpleShopD.Application.Queries.Products.GetAll;

internal sealed class GetAllProductsQueryHandler : IQueryHandler<GetAllProducts, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> HandleAsync(GetAllProducts query)
        => (await _productRepository.GetAllAsync()).Select(x => new ProductDto(x.Id, x.Title, x.Description, x.TypeOfProduct.ToString(), x.Price));
}