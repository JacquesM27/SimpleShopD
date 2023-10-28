namespace SimpleShopD.Application.Dto
{
    public sealed record OrderBasicDto(Guid OrderId, DateTime CreationDate, string CurrentStatus, decimal Total);
}
