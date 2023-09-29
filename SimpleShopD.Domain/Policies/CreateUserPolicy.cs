using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Users;

namespace SimpleShopD.Domain.Policies
{
    public sealed class CreateUserPolicy : ICreateUserPolicy
    {
        public bool CanBeApplied(UserRole userRole)
            => userRole == UserRole.User;

        public bool CanCreate(User? user)
            => true;
    }
}
