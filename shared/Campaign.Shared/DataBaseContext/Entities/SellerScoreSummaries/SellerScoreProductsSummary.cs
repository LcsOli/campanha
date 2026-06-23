namespace Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries
{
    public class SellerScoreProductsSummary : SellerScoreSummary
    {
        public int ProductId { get; private set; } = default!;

        public SellerScoreProductsSummary(int sellerId,
                                          int clientId,
                                          int productId,
                                          decimal points,
                                          int promotionCode)
        {
            Points = points;
            SellerId = sellerId;
            ClientId = clientId;
            ProductId = productId;
            PromotionCode = promotionCode;
        }
    }
}
