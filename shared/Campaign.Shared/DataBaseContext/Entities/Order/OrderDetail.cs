namespace Campaign.Shared.DataBaseContext.Entities.Order
{
    public class OrderDetail
    {
        public int Id { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal Price { get; private set; }
        public int CustomerId { get; private set; }
        public int ProductId { get; private set; }
        public DateTime DateOfSale { get; private set; }
        public int SellerId { get; private set; }

        public OrderDetail(int id, 
                           int sellerId,
                           decimal price, 
                           int productId, 
                           int customerId, 
                           decimal quantity, 
                           DateTime dateOfSale)
        {
            Id = id;
            Price = price;
            SellerId = sellerId;
            Quantity = quantity;
            ProductId = productId;
            DateOfSale = dateOfSale;
            CustomerId = customerId;
        }
    }
}
