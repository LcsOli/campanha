namespace Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries
{
    public class SellerScoreProductsSummary : SellerScoreSummary
    {
        public string ProductName { get; private set; } = default!;

        public SellerScoreProductsSummary(int sellerId,
                                          decimal points,
                                          int promotionCode,
                                          string clientName,
                                          string productName)
        {
            Points = points;
            SellerId = sellerId;
            ClientName = clientName;
            ProductName = productName;
            PromotionCode = promotionCode;
        }
    }
}
