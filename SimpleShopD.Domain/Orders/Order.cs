﻿using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Orders.Exceptions;
using SimpleShopD.Domain.Orders.ValueObjects;
using SimpleShopD.Domain.Shared.Base;
using SimpleShopD.Domain.Shared.ValueObjects;
using SimpleShopD.Domain.Users;

namespace SimpleShopD.Domain.Orders
{
    public sealed class Order : AggregateRoot<Guid>
    {
        public DateTime CreationDate { get; }
        public DateTime LastModifiedDate { get; private set; }
        public User User { get; private set; }
        public Address DeliveryAddress { get; private set; }
        public Fullname ReceiverFullname { get; private set; }
        public StatusOfOrder CurrentStatus { get; private set; }
        public IList<OrderLine> OrderLines { get; private set; }

        private decimal TotalPrice { get => OrderLines.Sum(x => x.SalePrice); }
        private int NextNo { get => OrderLines.Max(x => x.No); }

        public Order(Guid id, DateTime creationDate, User user, Address deliveryAddress, Fullname receiverFullname, IList<OrderLine> orderLines) : base(id)
        {
            CreationDate = creationDate;
            User = user;
            LastModifiedDate = DateTime.UtcNow;
            DeliveryAddress = deliveryAddress;
            ReceiverFullname = receiverFullname;
            CurrentStatus = OrderStatus.NotPaid;
            OrderLines = orderLines;
        }

        public void PayOrder(decimal amount)
        {
            if (CurrentStatus.Value is not OrderStatus.NotPaid)
                throw new PayOrderException("Order is already paid.");
            if (amount != TotalPrice)
                throw new PayOrderException("Invalid amount.");
            CurrentStatus = OrderStatus.Paid;
            LastModifiedDate = DateTime.UtcNow;
        }

        public void ChangeDeliveryAddress(Address address)
        {
            if (CurrentStatus.Value is not OrderStatus.NotPaid or not OrderStatus.Paid)
                throw new ChangeAddressException("Order is pending od completed. Cannot change delivery address.");
            DeliveryAddress = address;
        }

        public void ChangeReceiverFullname(Fullname receiverFullname)
        {
            if (CurrentStatus.Value is not OrderStatus.NotPaid or not OrderStatus.Paid)
                throw new ChangeReceiverOfOrderException("Order is pending or completed. Cannot chage order receiver");
            ReceiverFullname = receiverFullname;
        }

        public void MoveToPending()
        {
            if (CurrentStatus.Value is OrderStatus.NotPaid)
                throw new ChangeStatusOfOrderException("Order is not paid.");
            else if (CurrentStatus.Value is not OrderStatus.Paid)
                throw new ChangeStatusOfOrderException("Invalid order status.");

            CurrentStatus = OrderStatus.Pending;
            LastModifiedDate = DateTime.UtcNow;
        }

        public void MoveToComplete()
        {
            if (CurrentStatus.Value is not OrderStatus.Pending)
                throw new ChangeStatusOfOrderException("Order is not pending.");

            CurrentStatus = OrderStatus.Completed;
            LastModifiedDate = DateTime.UtcNow;
        }

        public void ReturnOrder()
        {
            if (CurrentStatus.Value is not OrderStatus.Completed)
                throw new ChangeStatusOfOrderException($"Cannot return order with status {CurrentStatus.Value}.");

            CurrentStatus = OrderStatus.Returned;
            LastModifiedDate = DateTime.UtcNow;
        }

        public void CancelOrder()
        {
            if (CurrentStatus.Value is not OrderStatus.NotPaid and not OrderStatus.Paid)
                throw new ChangeStatusOfOrderException($"Cannot cancel order with status {CurrentStatus.Value}.");

            CurrentStatus = OrderStatus.Cancelled;
            LastModifiedDate = DateTime.UtcNow;
        }

        public void AddOrderLine(Guid id, Guid productId, decimal quantity, decimal price)
        {
            OrderLine orderLine = new(id, NextNo, price, quantity, productId);
            AddOrderLine(orderLine);
        }

        private void AddOrderLine(OrderLine orderLine)
        {
            var existingLine = OrderLines.FirstOrDefault(x => x.ProductId.Equals(orderLine.ProductId) && x.SalePrice == orderLine.SalePrice);

            if (existingLine is not null)
                existingLine.AddQuantity(orderLine.Quantity);
            else
                OrderLines.Add(orderLine);

            LastModifiedDate = DateTime.UtcNow;
        }

        public void RemoveOrderLine(int no)
        {
            var orderLine = OrderLines.First(x => x.No == no)
                ?? throw new RemoveLineException("Line was not found.");

            RemoveLine(orderLine);
        }

        private void RemoveLine(OrderLine orderLine)
        {
            if (CurrentStatus.Value is not OrderStatus.NotPaid)
                throw new RemoveLineException($"Cannot remove line when status is {CurrentStatus.Value}.");

            int orderLineIndex = OrderLines.IndexOf(orderLine);
            if (orderLineIndex == -1)
                throw new RemoveLineException("Line was not found.");

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
