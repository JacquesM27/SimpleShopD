using SimpleShopD.Domain.Enum;

namespace SimpleShopD.Domain.Models.Users.ValueObjects
{
    internal readonly record struct Role(UserRole UserRole)
    {
        public static implicit operator UserRole(Role role) => role.UserRole;

        public static implicit operator Role(UserRole role) => new(role);
    }
}
