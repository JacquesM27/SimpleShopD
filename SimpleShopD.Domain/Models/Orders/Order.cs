using SimpleShopD.Domain.Models.Orders.ValueObjects;
using SimpleShopD.Domain.Models.Shared.Base;
using SimpleShopD.Domain.Models.Shared.ValueObjects;

namespace SimpleShopD.Domain.Models.Orders
{
    internal sealed class Order<T> : AggregateRoot<T> where T : notnull
    {
        public DateTime CreationDate { get; }
        public DateTime LastModifiedDate { get; private set; }
        public Address DeliveryAddress { get; }
        public Fullname ReceiverFullname { get; }
        public Status OrderStatus { get; private set; }
        // TODO : Add order lines

        public Order(T id, DateTime creationDate, DateTime lastModifiedDate, Address deliveryAddress, Fullname receiverFullname, Status orderStatus): base(id)
        {
            CreationDate = creationDate;
            LastModifiedDate = lastModifiedDate;
            DeliveryAddress = deliveryAddress;
            ReceiverFullname = receiverFullname;
            OrderStatus = orderStatus;
        }
    }
}
