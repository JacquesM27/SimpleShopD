namespace SimpleShopD.Application.Commands.Orders.SharedDto
{
    public sealed record OrderLine(Guid ProductId, decimal Quantity, decimal Price);
}
