using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Products.Delete
{
    public sealed record DeleteProduct(Guid ProductId) : ICommand;
}
