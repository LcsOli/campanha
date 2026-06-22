namespace Campaign.Shared.DataBaseContext.Entities.Summaries
{
    public abstract class SellerScoreSummaries
    {
        public int Id { get; protected set; }
        public int SellerId { get; protected set; }
        public int PromotionCode { get; protected set; }
        public decimal Points { get; protected set; }
    }
}
