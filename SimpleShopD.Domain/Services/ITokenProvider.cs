using SimpleShopD.Domain.Users.ValueObjects;

namespace SimpleShopD.Domain.Services
{
    public interface ITokenProvider
    {
        string Provide(Role role, Guid userId, string email);
        string GenerateRandomToken();
    }
}
