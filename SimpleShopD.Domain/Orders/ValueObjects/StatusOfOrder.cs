using SimpleShopD.Domain.Enum;

namespace SimpleShopD.Domain.Orders.ValueObjects
{
    public sealed record StatusOfOrder(OrderStatus Value)
    {
        public static implicit operator OrderStatus(StatusOfOrder status) => status.Value;

        public static implicit operator StatusOfOrder(OrderStatus status) => new(status);
    }
}
