using SimpleShopD.Domain.Enum;

namespace SimpleShopD.Domain.Users.ValueObjects
{
    public sealed record Status(AccountStatus Value)
    {

        public static implicit operator AccountStatus(Status status) => status.Value;

        public static implicit operator Status(AccountStatus status) => new(status);
    }
}
