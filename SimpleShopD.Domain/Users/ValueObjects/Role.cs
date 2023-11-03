namespace SimpleShopD.Domain.Users.ValueObjects
{
    public sealed record Role(string Value)
    {
        public const string User = nameof(User);
        public const string Admin = nameof(Admin);

        public static implicit operator Role(string Value) => new(Value);

        public static implicit operator string(Role userRole) => userRole.Value;
    }
}
