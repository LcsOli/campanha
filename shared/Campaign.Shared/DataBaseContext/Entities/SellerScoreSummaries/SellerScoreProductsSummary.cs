namespace Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries
{
    public class SellerScoreProductsSummary : SellerScoreSummary
    {
        public int ProductId { get; private set; } = default!;

        public SellerScoreProductsSummary(int sellerId,
                                          int productId,
                                          int customerId,
                                          decimal score,
                                          int promotionCode)
        {
            Score = score;
            SellerId = sellerId;
            ProductId = productId;
            CustomerId = customerId;
            PromotionCode = promotionCode;
        }
    }
}
