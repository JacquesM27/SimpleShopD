using SimpleShopD.Application.Dto;
using SimpleShopD.Shared.Abstractions.Queries;

namespace SimpleShopD.Application.Queries.Products.GetAll;

public sealed record GetAllProducts() : IQuery<IEnumerable<ProductDto>>;