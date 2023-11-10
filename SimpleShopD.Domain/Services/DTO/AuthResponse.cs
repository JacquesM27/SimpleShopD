namespace SimpleShopD.Domain.Services.DTO
{
    public readonly record struct AuthResponse(string Jwt, string Fullname);
}
