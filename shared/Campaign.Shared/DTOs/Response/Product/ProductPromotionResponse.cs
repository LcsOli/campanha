namespace Campaign.Shared.DTOs.Response.Product
{
    public class ProductPromotionResponse
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int PromotionCode { get; private set; }
        public decimal? QuantityPointsGoals { get; private set; }
        public ProductPromotionResponse(int id,
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
