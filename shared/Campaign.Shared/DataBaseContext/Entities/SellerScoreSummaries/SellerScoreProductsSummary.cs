namespace Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries
{
    public class SellerScoreProductsSummary : SellerScoreSummary
    {
        public int ProductId { get; private set; } = default!;
        public bool IsCanceled { get; private set; } = false;

        public SellerScoreProductsSummary(int sellerId,
                                          decimal score,
                                          int productId,
                                          int customerId,
                                          bool isCanceled,
                                          int promotionCode)
        {
            Score = score;
            SellerId = sellerId;
            ProductId = productId;
            CustomerId = customerId;
            IsCanceled = isCanceled;
            PromotionCode = promotionCode;
        }
    }
}
