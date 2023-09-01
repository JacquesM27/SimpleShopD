namespace SimpleShopD.Domain.Services.POCO
{
    public readonly record struct LoginToken(string Value, DateTime ExpirationDate);
}
