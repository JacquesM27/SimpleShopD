using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Services.POCO;

namespace SimpleShopD.Domain.Services
{
    internal interface ITokenProvider<T> where T : notnull
    {
        LoginToken Provide(DateTime expirationDate, UserRole role, T userId);
    }
}
