namespace SimpleShopD.Domain.Services.DTO
{
    public readonly record struct AuthToken(string Refresh, DateTime RefreshExpirationDate, string? Jwt);
}
