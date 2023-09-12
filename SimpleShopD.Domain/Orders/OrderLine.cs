using SimpleShopD.Domain.Orders.Exceptions;
using SimpleShopD.Doman.Shared.Base;

namespace SimpleShopD.Domain.Orders
{
    public class OrderLine<T> : Entity<T> where T : notnull
    {
        public int No { get; private set; }
        public decimal SalePrice { get; }
        public decimal Quantity { get; private set; }
        public T ProductId { get; }

        public OrderLine(T id, int no, decimal salePrice, decimal quantity, T productId) 
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
