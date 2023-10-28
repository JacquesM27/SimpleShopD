namespace SimpleShopD.Application.Dto
{
    public sealed record OrderLine(Guid ProductId, decimal Quantity, decimal Price);
}
