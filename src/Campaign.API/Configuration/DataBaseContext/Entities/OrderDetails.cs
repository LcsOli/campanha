namespace Campaign.API.Configuration.DataBaseContext.Entities
{
    public class OrderDetails
    {
        public int Id { get; private set; }
        public long Quantity { get; private set; }
        public int Price { get; private set; }
        public int CustomerId { get; private set; }
        public int ProductId { get; private set; }
    }
}
