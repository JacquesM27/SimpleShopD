using SimpleShopD.Domain.Enum;

namespace SimpleShopD.Doman.Users.ValueObjects
{
    public sealed record RoleOfUser(UserRole UserRole)
    {
        public static implicit operator UserRole(RoleOfUser role) => role.UserRole;

        public static implicit operator RoleOfUser(UserRole role) => new(role);
    }
}
