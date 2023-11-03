using SimpleShopD.Domain.Enum;

namespace SimpleShopD.Domain.Orders.ValueObjects
{
    public sealed record Status(OrderStatus Value)
    {
        public static implicit operator OrderStatus(Status status) => status.Value;

        public static implicit operator Status(OrderStatus status) => new(status);
    }
}
