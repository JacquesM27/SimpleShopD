namespace SimpleShopD.Domain.Services.DTO
{
    public readonly record struct LoginToken(string Value, DateTime ExpirationDate);
}
