namespace Campaign.Shared.DataBaseContext.Entities.Seller
{
    public class SellerScore
    {
        public int Id { get; private set; }
        public int SellerId { get; private set; }
        public decimal Score { get; private set; }
        public string Name { get; private set; } = default!;
        public string ManagerName { get; private set; } = default!;

        public SellerScore(string name, 
                           int sellerId, 
                           string managerName)
        {
            Name = name;
            SellerId = sellerId;
            ManagerName = managerName;
        }

        public SellerScore(int id, 
                           string name, 
                           int sellerId, 
                           decimal score, 
                           string managerName)
        {
            Id = id;
            Name = name;
            Score = score;
            SellerId = sellerId;
            ManagerName = managerName;
        }

        public void UpdateScore(decimal score)
        {
            Score += score;
        }
    }
}
