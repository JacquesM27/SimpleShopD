using SimpleShopD.Domain.Enum;

namespace SimpleShopD.Domain.Models.Orders.ValueObjects
{
    internal class Status
    {
        public OrderStatus OrderStatus { get; private set; }

        public Status(OrderStatus orderStatus)
        {
            OrderStatus = orderStatus;
        }

        internal void SetStatus(OrderStatus orderStatus) => OrderStatus = orderStatus;
    }
}
