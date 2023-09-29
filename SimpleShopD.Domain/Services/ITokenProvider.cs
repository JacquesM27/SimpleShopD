using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Services.DTO;

namespace SimpleShopD.Domain.Services
{
    public interface ITokenProvider
    {
        LoginToken Provide(DateTime expirationDate, UserRole role, Guid userId);
    }
}
