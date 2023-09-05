namespace SimpleShopD.Domain.Models.Orders.ValueObjects
{
    internal class OrderLine<T> where T : notnull
    {
        public int No { get; private set; }
        public decimal SalePrice { get; }
        public decimal Quantity { get; }
        public T ProductId { get; }

        public OrderLine(int no, decimal salePrice, decimal quantity, T productId)
        {
            if(no < 0) 
                throw new ArgumentOutOfRangeException(nameof(no));
            if(salePrice < 0)
                throw new ArgumentOutOfRangeException(nameof(salePrice));
            if (quantity < 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            No = no;
            SalePrice = salePrice;
            Quantity = quantity;
            ProductId = productId;
        }

        internal void SetNo(int no)
        {
            if(no < 0)
                throw new ArgumentOutOfRangeException(nameof(no));
            No = no;
        }
    }
}
