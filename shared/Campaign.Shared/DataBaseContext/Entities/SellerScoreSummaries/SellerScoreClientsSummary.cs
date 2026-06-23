namespace Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries
{
    public class SellerScoreClientsSummaries : SellerScoreSummary
    {
        public DateTime RegisteredIn { get; private set; }
        public DateTime ReactivatedIn { get; private set; }

        public SellerScoreClientsSummaries(int sellerId,
                                           int clientId,
                                           decimal points,
                                           int promotionCode,
                                           DateTime registeredIn,
                                           DateTime reactivatedIn)
        {
            Points = points;
            SellerId = sellerId;
            ClientId = clientId;
            RegisteredIn = registeredIn;
            ReactivatedIn = reactivatedIn;
            PromotionCode = promotionCode;
        }
    }
}
