namespace SimpleShopD.Application.Commands.SharedDto
{
    public sealed record OrderLine(Guid ProductId, decimal Quantity, decimal Price);
}
