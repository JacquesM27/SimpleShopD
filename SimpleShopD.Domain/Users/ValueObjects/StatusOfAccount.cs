using SimpleShopD.Domain.Enum;

namespace SimpleShopD.Domain.Users.ValueObjects
{
    public sealed record StatusOfAccount(AccountStatus Value)
    {

        public static implicit operator AccountStatus(StatusOfAccount status) => status.Value;

        public static implicit operator StatusOfAccount(AccountStatus status) => new(status);
    }
}
