using SimpleShopD.Domain.Services.POCO;

namespace SimpleShopD.Domain.Services
{
    internal interface ITokenProvider<T> where T : notnull
    {
        LoginToken Provide(DateTime expirationDate, string role, T userId);
    }
}
