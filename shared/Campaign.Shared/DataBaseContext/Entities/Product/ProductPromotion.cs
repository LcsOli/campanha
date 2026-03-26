namespace Campaign.Shared.DataBaseContext.Entities.Product
{
    public class ProductPromotion
    {
        public int Id { get; private set; }
        public DateTime CreateAt { get; private set; }
        public int PromotionCode { get; private set; }
        public int ProductId { get; private set; }
        public decimal QuantityPoints { get; private set; }
        public decimal QuantityGoals { get; private set; }
        public decimal QuantityPointsValue { get; private set; }
        public decimal QuantityPointsGoals { get; private set; }
        public decimal ValuePoints { get; private set; }
        public decimal MaxPointsValue { get; private set; }
        public decimal QuantityPointsWeight { get; private set; }
        public decimal MinPointsValue { get; private set; }
        public decimal QuantityMinWeightPoints { get; private set; }
        public decimal QuantityMinItem { get; private set; }

        public ProductPromotion(int id,
                                int productId,
                                int promotionCode,
                                DateTime createAt,
                                decimal valuePoints,
                                decimal quantityGoals,
                                decimal maxPointsValue,
                                decimal minPointsValue,
                                decimal quantityPoints,
                                decimal quantityMinItem,
                                decimal quantityPointsValue,
                                decimal quantityPointsGoals,
                                decimal quantityPointsWeight,
                                decimal quantityMinWeightPoints)
        {
            Id = id;
            CreateAt = createAt;
            ProductId = productId;
            ValuePoints = valuePoints;
            QuantityGoals = quantityGoals;
            PromotionCode = promotionCode;
            MaxPointsValue = maxPointsValue;
            MinPointsValue = minPointsValue;
            QuantityPoints = quantityPoints;
            QuantityMinItem = quantityMinItem;
            QuantityPointsValue = quantityPointsValue;
            QuantityPointsGoals = quantityPointsGoals;
            QuantityPointsWeight = quantityPointsWeight;
            QuantityMinWeightPoints = quantityMinWeightPoints;
        }
    }
}
