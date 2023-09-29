using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Users;

namespace SimpleShopD.Domain.Policies
{
    public interface ICreateUserPolicy
    {
        bool CanBeApplied(UserRole userRole);
        bool CanCreate(User? user);
    }
}
