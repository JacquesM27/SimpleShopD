using SimpleShopD.Domain.Orders.Exceptions;
using SimpleShopD.Domain.Orders.ValueObjects;
using SimpleShopD.Domain.Shared.Base;
using SimpleShopD.Domain.Shared.ValueObjects;

namespace SimpleShopD.Domain.Orders
{
    public class OrderLine : Entity<Guid>
    {
        public int No { get; private set; }
        public Price Price { get; }
        public Quantity Quantity { get; private set; }
        public Guid ProductId { get; }

        internal OrderLine(Guid id, int no, Price price, Quantity quantity, Guid productId)
            : base(id)
        {
            if (no < 0)
                throw new NegativeOrderLineNumberException(no.ToString());

            No = no;
            Price = price;
            Quantity = quantity;
            ProductId = productId;
        }

        internal void SetNo(int no)
        {
            if (no < 0)
                throw new NegativeOrderLineNumberException(no.ToString());
            No = no;
        }

        internal void AddQuantity(decimal quantity)
        {
            Quantity += quantity;
        }
    }
}
