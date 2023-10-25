using SimpleShopD.Domain.Enum;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Products.Add
{
    public sealed record AddProduct(string Title, string Description, ProductType ProductType, decimal Price) : ICommandTResult<Guid>;
}
