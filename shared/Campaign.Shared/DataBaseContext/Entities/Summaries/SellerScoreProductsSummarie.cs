namespace Campaign.Shared.DataBaseContext.Entities.Summaries
{
    public class SellerScoreProductsSummarie : SellerScoreSummaries
    {
        public string ProductName { get; private set; } = default!;

        public SellerScoreProductsSummarie(int sellerId,
                                           decimal points,
                                           int promotionCode,
                                           string productName)
        {
            Points = points;
            SellerId = sellerId;
            ProductName = productName;
            PromotionCode = promotionCode;
        }
    }
}
