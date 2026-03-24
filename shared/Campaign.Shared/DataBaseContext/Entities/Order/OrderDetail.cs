namespace Campaign.Shared.DataBaseContext.Entities.Order
{
    public class OrderDetail
    {
        public int Id { get; private set; }
        public long Quantity { get; private set; }
        public int Price { get; private set; }
        public int CustomerId { get; private set; }
        public int ProductId { get; private set; }
    }
}
