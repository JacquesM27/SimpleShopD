using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Services.DTO;

namespace SimpleShopD.Domain.Services
{
    public interface ITokenProvider<T> where T : notnull
    {
        LoginToken Provide(DateTime expirationDate, UserRole role, T userId);
    }
}
