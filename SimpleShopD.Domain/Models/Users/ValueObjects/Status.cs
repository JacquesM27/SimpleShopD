using SimpleShopD.Domain.Enum;

namespace SimpleShopD.Domain.Models.Users.ValueObjects
{
    internal readonly record struct Status
    {
        public AccountStatus Value { get; }

        public Status(AccountStatus value)
        {
            Value = value;
        }

        public static implicit operator AccountStatus(Status status) => status.Value;

        public static implicit operator Status(AccountStatus status) => new(status);
    }
}
