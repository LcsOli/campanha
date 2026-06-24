using Campaign.Shared.Enums.SellerScoreConsumerType;

namespace Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries
{
    public class SellerScoreClientsSummary : SellerScoreSummary
    {
        public DateTime RegisteredIn { get; private set; }
        public DateTime ReactivatedIn { get; private set; }
        public CustomerSalesEventType CustomerSalesEventType { get; private set; }

        public SellerScoreClientsSummary(int sellerId,
                                           int customerId,
                                           decimal points,
                                           int promotionCode,
                                           DateTime registeredIn,
                                           DateTime reactivatedIn,
                                           CustomerSalesEventType customerSalesEventType)
        {
            Points = points;
            SellerId = sellerId;
            CustomerId = customerId;
            RegisteredIn = registeredIn;
            ReactivatedIn = reactivatedIn;
            PromotionCode = promotionCode;
            CustomerSalesEventType = customerSalesEventType;
        }
    }
}
