using SimpleShopD.Application.Dto;
using SimpleShopD.Shared.Abstractions.Queries;

namespace SimpleShopD.Application.Queries.Products.Get;

public sealed record GetProduct(Guid ProductId) : IQuery<ProductDto>;