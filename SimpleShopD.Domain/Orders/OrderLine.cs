using SimpleShopD.Domain.Orders.Exceptions;
using SimpleShopD.Domain.Shared.Base;

namespace SimpleShopD.Domain.Orders
{
    public class OrderLine : Entity<Guid>
    {
        public int No { get; private set; }
        public decimal SalePrice { get; }
        public decimal Quantity { get; private set; }
        public Guid ProductId { get; }

        internal OrderLine(Guid id, int no, decimal salePrice, decimal quantity, Guid productId)
            : base(id)
        {
            if (no < 0)
                throw new InvalidOrderLineArgumentException(nameof(no));
            if (salePrice < 0)
                throw new InvalidOrderLineArgumentException(nameof(salePrice));
            if (quantity < 0)
                throw new InvalidOrderLineArgumentException(nameof(quantity));

            No = no;
            SalePrice = salePrice;
            Quantity = quantity;
            ProductId = productId;
        }

        internal void SetNo(int no)
        {
            if (no < 0)
                throw new InvalidOrderLineArgumentException(nameof(no));
            No = no;
        }

        internal void AddQuantity(decimal quantity)
        {
            Quantity += quantity;
        }
    }
}
