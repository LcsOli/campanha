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
    }
}
