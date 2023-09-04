using SimpleShopD.Domain.Enum;

namespace SimpleShopD.Domain.Models.Orders.ValueObjects
{
    internal readonly record struct Status(OrderStatus OrderStatus)
    {
        public static implicit operator OrderStatus(Status status) => status.OrderStatus;

        public static implicit operator Status(OrderStatus orderStatus) => new(orderStatus);
    }
}
