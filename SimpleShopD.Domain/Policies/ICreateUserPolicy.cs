using SimpleShopD.Domain.Users;
using SimpleShopD.Domain.Users.ValueObjects;

namespace SimpleShopD.Domain.Policies
{
    public interface ICreateUserPolicy
    {
        bool CanBeApplied(Role userRole);
        bool CanCreate(User? user);
    }
}
