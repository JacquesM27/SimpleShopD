using SimpleShopD.Domain.Users.ValueObjects;

namespace SimpleShopD.Domain.Services
{
    public interface ITokenProvider
    {
        string Provide(DateTime expirationDate, Role role, Guid userId, string email);
    }
}
