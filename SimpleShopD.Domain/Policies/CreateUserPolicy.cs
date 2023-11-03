using SimpleShopD.Domain.Users;
using SimpleShopD.Domain.Users.ValueObjects;

namespace SimpleShopD.Domain.Policies
{
    public sealed class CreateUserPolicy : ICreateUserPolicy
    {
        public bool CanBeApplied(Role userRole)
            => userRole == Role.User;

        public bool CanCreate(User? user)
            => true;
    }
}
