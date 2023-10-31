namespace SimpleShopD.Application.Dto
{
    public sealed record OrderDto(Guid OrderId, AddressDto Address, string FirstName, string LastName, string Status,
        IEnumerable<LineDto> Lines);

    public sealed record LineDto(Guid LineId, int No, string ProductName, decimal Quantity, decimal Price);
}