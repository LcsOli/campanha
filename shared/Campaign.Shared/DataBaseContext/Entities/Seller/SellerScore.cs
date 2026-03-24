namespace Campaign.Shared.DataBaseContext.Entities.Seller
{
    public class SellerScore
    {
        public int Id { get; private set; }
        public int SellerId { get; private set; }
        public decimal Score { get; private set; }
        public string Name { get; private set; } = default!;
        public string ManagerName { get; private set; } = default!;
        public Users.User? User { get; private set; }
    }
}
