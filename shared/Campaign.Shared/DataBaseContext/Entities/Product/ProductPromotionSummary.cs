namespace Campaign.Shared.DataBaseContext.Entities.Product
{
    public class ProductPromotionSummary
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int PromotionCode { get; private set; }
        public decimal? QuantityPointsGoals { get; private set; }
        public ProductPromotionSummary(int id,
                                       int productId,
                                       int promotionCode,
                                       decimal? quantityPointsGoals)
        {
            Id = id;
            ProductId = productId;
            PromotionCode = promotionCode;
            QuantityPointsGoals = quantityPointsGoals;
        }
    }
}
