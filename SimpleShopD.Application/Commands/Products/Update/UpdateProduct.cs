using SimpleShopD.Domain.Enum;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Products.Update
{
    public sealed record UpdateProduct(Guid Id, string Title, string Description, ProductType ProductType, decimal Price) : ICommand;
}
