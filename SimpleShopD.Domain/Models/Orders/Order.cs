using SimpleShopD.Domain.Enum;
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
        public Status CurrentStatus { get; private set; }
        public IList<OrderLine<T>> OrderLines { get; private set; }

        private decimal TotalPrice { get => OrderLines.Sum(x => x.SalePrice); }
        private int NextLineNo { get => OrderLines.Max(x => x.No) + 1; }

        public Order(T id, DateTime creationDate, Address deliveryAddress, Fullname receiverFullname, IEnumerable<OrderLine<T>> orderLines) : base(id)
        {
            CreationDate = creationDate;
            LastModifiedDate = DateTime.UtcNow;
            DeliveryAddress = deliveryAddress;
            ReceiverFullname = receiverFullname;
            CurrentStatus = new(OrderStatus.NotPaid);
            OrderLines = orderLines.ToList();
        }

        public void PayOrder(decimal amount)
        {
            if(CurrentStatus.OrderStatus is not OrderStatus.NotPaid)
                throw new InvalidOperationException("Order is already paid.");
            if(amount != TotalPrice)
                throw new InvalidOperationException("Invalid amount.");
            CurrentStatus.SetStatus(OrderStatus.Paid);
            LastModifiedDate = DateTime.UtcNow;
        }

        public void MoveToPending()
        {
            if (CurrentStatus.OrderStatus is OrderStatus.NotPaid)
                throw new InvalidOperationException("Order is not paid.");
            else if (CurrentStatus.OrderStatus is not OrderStatus.Paid)
                throw new InvalidOperationException("Invalid order status.");

            CurrentStatus.SetStatus(OrderStatus.Pending);
            LastModifiedDate = DateTime.UtcNow;
        }

        public void MoveToComplete()
        {
            if (CurrentStatus.OrderStatus is not OrderStatus.Pending)
                throw new InvalidOperationException("Order is not pending.");

            CurrentStatus.SetStatus(OrderStatus.Completed);
            LastModifiedDate = DateTime.UtcNow;
        }

        public void ReturnOrder()
        {
            if (CurrentStatus.OrderStatus is not OrderStatus.Completed)
                throw new InvalidOperationException($"Cannot return order with status {CurrentStatus.OrderStatus}.");

            CurrentStatus.SetStatus(OrderStatus.Returned);
            LastModifiedDate = DateTime.UtcNow;
        }

        public void CancelOrder()
        {
            if (CurrentStatus.OrderStatus is not OrderStatus.NotPaid and not OrderStatus.Paid)
                throw new InvalidOperationException($"Cannot cancel order with status {CurrentStatus.OrderStatus}.");

            CurrentStatus.SetStatus(OrderStatus.Canceled);
            LastModifiedDate = DateTime.UtcNow;
        }

        public void AddOrderLine(decimal salePrice, decimal quantity, T productId)
        {
            OrderLines.Add(new OrderLine<T>(NextLineNo, salePrice, quantity, productId));
            LastModifiedDate = DateTime.UtcNow;
        }

        public void RemoveOrderLine(int no)
        {
            if(!OrderLines.Any(x => x.No == no))
                throw new InvalidOperationException("Line was not found.");

            int indexOfNo = OrderLines.IndexOf(OrderLines.First(x => x.No == no));
            RemoveLine(indexOfNo);
        }

        public void RemoveOrderLine(OrderLine<T> orderLine)
        {
            int orderLineIndex = OrderLines.IndexOf(orderLine);
            if (orderLineIndex == -1)
                throw new InvalidOperationException("Line was not found.");
            RemoveLine(orderLineIndex);
        }

        private void RemoveLine(int orderLineIndex)
        {
            OrderLines.RemoveAt(orderLineIndex);

            if (orderLineIndex != OrderLines.Count - 1)
                UpdateLinesNos(orderLineIndex);

            LastModifiedDate = DateTime.UtcNow;
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
