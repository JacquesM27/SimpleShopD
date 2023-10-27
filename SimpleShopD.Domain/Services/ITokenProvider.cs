using SimpleShopD.Domain.Enum;

namespace SimpleShopD.Domain.Services
{
    public interface ITokenProvider
    {
        string Provide(DateTime expirationDate, UserRole role, Guid userId, string email);
    }
}
