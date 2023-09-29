using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Users;

namespace SimpleShopD.Domain.Policies
{
    public sealed class CreateAdminPolicy : ICreateUserPolicy
    {
        public bool CanBeApplied(UserRole userRole)
            => userRole == UserRole.Admin;

        public bool CanCreate(User? user)
            => user is not null && user.RoleOfUser == UserRole.Admin;
    }
}
