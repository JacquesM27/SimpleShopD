using SimpleShopD.Domain.Users;
using SimpleShopD.Domain.Users.ValueObjects;

namespace SimpleShopD.Domain.Policies
{
    public sealed class CreateAdminPolicy : ICreateUserPolicy
    {
        public bool CanBeApplied(Role userRole)
            => userRole == Role.Admin;

        public bool CanCreate(User? user)
            => user is not null && Equals(user.UserRole.Value, Role.Admin);
    }
}
