namespace Campaign.Shared.DataBaseContext.Entities.Seller
{
    public class SellerScore
    {
        public int Id { get; private set; }
        public int SellerId { get; private set; }
        public decimal Score { get; private set; }
        public string Name { get; private set; } = default!;
        public string ManagerName { get; private set; } = default!;
        public int TeamId { get; private set; }
        public Team.Team? Team { get; private set; }
        public short Coupons { get; private set; }
        public decimal RevenueTarget { get; private set; }
        public decimal CurrentRevenue { get; private set; }
        public decimal RevenueMonth1 { get; private set; }
        public decimal RevenueMonth2 { get; private set; }
        public decimal RevenueMonth3 { get; private set; }
        public decimal RevenueMonth4 { get; private set; }
        public decimal RevenueMonth5 { get; private set; }

        public SellerScore(string name, 
                           int sellerId, 
                           string managerName)
        {
            Name = name;
            SellerId = sellerId;
            ManagerName = managerName;
        }

        public void UpdateScore(decimal score)
        {
            Score += score;
        }

        public void UpdateCoupons(short coupons)
        {
            Coupons = coupons;
        }
    }
}
