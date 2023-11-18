using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Orders.Exceptions;
using SimpleShopD.Domain.Orders.ValueObjects;
using SimpleShopD.Domain.Shared.Base;
using SimpleShopD.Domain.Shared.ValueObjects;

namespace SimpleShopD.Domain.Orders
{
    public sealed class Order : AggregateRoot<Guid>
    {
        public DateTime CreationDate { get; }
        public DateTime LastModifiedDate { get; private set; }
        public Guid UserId { get; private set; }
        public Guid AddressId { get; private set; }
        public Fullname ReceiverFullname { get; private set; }
        public Status Status { get; private set; }
        public IList<OrderLine> OrderLines { get; private set; }

        private decimal TotalPrice { get => OrderLines.Sum(x => x.Price); }
        private int NextNo { get => OrderLines.Max(x => x.No) + 1; }

        public Order(Guid id, Guid userId, Guid addressId, Fullname receiverFullname) : base(id)
        {
            DateTime now = DateTimeOffset.UtcNow.UtcDateTime;
            CreationDate = now;
            UserId = userId;
            LastModifiedDate = now;
            AddressId = addressId;
            ReceiverFullname = receiverFullname;
            Status = OrderStatus.NotPaid;
            OrderLines = new List<OrderLine>();
        }

        private Order() : base(Guid.NewGuid()) { }

        public void PayOrder(decimal amount)
        {
            if (Status.Value is not OrderStatus.NotPaid)
                throw new PayOrderException("Order is already paid.");
            if (amount != TotalPrice)
                throw new PayOrderException("Invalid amount.");
            Status = OrderStatus.Paid;
            LastModifiedDate = DateTimeOffset.UtcNow.UtcDateTime;
        }

        public void ChangeDeliveryAddress(Guid addressId)
        {
            if (Status.Value is not OrderStatus.NotPaid or not OrderStatus.Paid)
                throw new ChangeAddressException("Order is pending od completed. Cannot change delivery address.");
            AddressId = addressId;
        }

        public void ChangeReceiverFullname(Fullname receiverFullname)
        {
            if (Status.Value is not OrderStatus.NotPaid or not OrderStatus.Paid)
                throw new ChangeReceiverOfOrderException("Order is pending or completed. Cannot chage order receiver");
            ReceiverFullname = receiverFullname;
        }

        public void MoveToPending()
        {
            if (Status.Value is OrderStatus.NotPaid)
                throw new ChangeStatusOfOrderException("Order is not paid.");
            else if (Status.Value is not OrderStatus.Paid)
                throw new ChangeStatusOfOrderException("Invalid order status.");

            Status = OrderStatus.Pending;
            LastModifiedDate = DateTimeOffset.UtcNow.UtcDateTime;
        }

        public void MoveToComplete()
        {
            if (Status.Value is not OrderStatus.Pending)
                throw new ChangeStatusOfOrderException("Order is not pending.");

            Status = OrderStatus.Completed;
            LastModifiedDate = DateTimeOffset.UtcNow.UtcDateTime;
        }

        public void ReturnOrder()
        {
            if (Status.Value is not OrderStatus.Completed)
                throw new ChangeStatusOfOrderException($"Cannot return order with status {Status.Value}.");

            Status = OrderStatus.Returned;
            LastModifiedDate = DateTimeOffset.UtcNow.UtcDateTime;
        }

        public void CancelOrder()
        {
            if (Status.Value is not OrderStatus.NotPaid and not OrderStatus.Paid)
                throw new ChangeStatusOfOrderException($"Cannot cancel order with status {Status.Value}.");

            Status = OrderStatus.Cancelled;
            LastModifiedDate = DateTimeOffset.UtcNow.UtcDateTime;
        }

        public void AddOrderLine(Guid id, Guid productId, decimal quantity, decimal price)
        {
            OrderLine orderLine = new(id, NextNo, price, quantity, productId);
            AddOrderLine(orderLine);
        }

        private void AddOrderLine(OrderLine orderLine)
        {
            var existingLine = OrderLines.FirstOrDefault(x => x.ProductId.Equals(orderLine.ProductId) && x.Price == orderLine.Price);

            if (existingLine is not null)
                existingLine.AddQuantity(orderLine.Quantity);
            else
                OrderLines.Add(orderLine);

            LastModifiedDate = DateTimeOffset.UtcNow.UtcDateTime;
        }

        public void RemoveOrderLine(Guid id)
        {
            var orderLine = OrderLines.First(x => x.Id == id)
                ?? throw new RemoveLineException("Line was not found.");

            RemoveLine(orderLine);
        }

        public decimal GetTotal() 
            => TotalPrice;

        private void RemoveLine(OrderLine orderLine)
        {
            if (Status.Value is not OrderStatus.NotPaid)
                throw new RemoveLineException($"Cannot remove line when status is {Status.Value}.");

            int orderLineIndex = OrderLines.IndexOf(orderLine);
            if (orderLineIndex == -1)
                throw new RemoveLineException("Line was not found.");

            OrderLines.RemoveAt(orderLineIndex);

            if (orderLineIndex != OrderLines.Count - 1)
                UpdateLinesNos(orderLineIndex);

            LastModifiedDate = DateTimeOffset.UtcNow.UtcDateTime;
        }

        private void UpdateLinesNos(int startIndex)
        {
            for (int i = startIndex; i < OrderLines.Count; i++)
            {
                OrderLines[i].SetNo(i + 1);
            }
        }
    }
}
