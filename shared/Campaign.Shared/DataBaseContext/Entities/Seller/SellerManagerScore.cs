namespace Campaign.Shared.DataBaseContext.Entities.Seller
{
    public class SellerManagerScore
    {
        public int Id { get; private set; }
        public int Code { get; private set; }
        public string Name { get; private set; } = default!;
        public decimal TargetRevenue { get; private set; }
        public decimal CurrentRevenue { get; private set; }
        public int SellerId { get; private set; }

        public SellerManagerScore(int id,
                                  int code, 
                                  string name,
                                  decimal targetRevenue,
                                  decimal currentRevenue)
        {
            Id = id;
            Code = code;
            Name = name;
            TargetRevenue = targetRevenue;
            CurrentRevenue = currentRevenue;
        }

        public void UpdateCurrentRevenue(decimal revenue)
        {
            CurrentRevenue = revenue;
        }
    }
}
