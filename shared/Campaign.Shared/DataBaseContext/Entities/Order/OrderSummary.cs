namespace Campaign.Shared.DataBaseContext.Entities.Order
{
    public class OrderSummary
    {
        public long Id { get; private set; }
        public int SellerId { get; private set; }
        public int CustomerId { get; private set; }
        public int BranchId { get; private set; }
        public DateTime DateOfSale { get; private set; }
        public Branch.Branch? Branch { get; private set; }
        public Seller.Seller? Seller { get; private set; }
        public Customer.Customer? Customer { get; private set; }
    }
}
