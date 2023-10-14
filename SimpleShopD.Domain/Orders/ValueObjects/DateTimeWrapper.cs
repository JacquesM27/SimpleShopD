namespace SimpleShopD.Domain.Orders.ValueObjects
{
    public sealed record DateTimeWrapper(DateTime Value)
    {
        public static implicit operator DateTimeWrapper(DateTime Value) => Value;
        public static implicit operator DateTime(DateTimeWrapper dateTimeWrapper) => dateTimeWrapper;
    }
}
