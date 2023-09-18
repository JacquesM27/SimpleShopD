using SimpleShopD.Domain.Enum;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Products
{
    public sealed record AddProduct(string Title, string Description, ProductType ProductType, decimal Price) : ICommand;
}
