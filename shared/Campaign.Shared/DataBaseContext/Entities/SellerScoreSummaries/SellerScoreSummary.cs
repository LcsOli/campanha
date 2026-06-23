namespace Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries
{
    public abstract class SellerScoreSummary
    {
        public int Id { get; protected set; }
        public int SellerId { get; protected set; }
        public int PromotionCode { get; protected set; }
        public decimal Points { get; protected set; }
        public int ClientId { get; protected set; } = default!;
    }
}
