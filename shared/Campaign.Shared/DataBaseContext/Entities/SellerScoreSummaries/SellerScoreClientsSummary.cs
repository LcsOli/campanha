namespace Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries
{
    public class SellerScoreClientsSummaries : SellerScoreSummary
    {
        public DateTime RegisteredIn { get; private set; }
        public DateTime ReactivatedIn { get; private set; }

        public SellerScoreClientsSummaries(int sellerId,
                                           decimal points,
                                           int promotionCode,
                                           string clientName,
                                           DateTime registeredIn,
                                           DateTime reactivatedIn)
        {
            Points = points;
            SellerId = sellerId;
            ClientName = clientName;
            RegisteredIn = registeredIn;
            ReactivatedIn = reactivatedIn;
            PromotionCode = promotionCode;
        }
    }
}
