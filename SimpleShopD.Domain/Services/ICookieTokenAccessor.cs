namespace SimpleShopD.Domain.Services
{
    public interface ICookieTokenAccessor
    {
        string? GetRefreshToken();

        void AppendRefreshToken(string refreshToken, DateTime expires);
    }
}
