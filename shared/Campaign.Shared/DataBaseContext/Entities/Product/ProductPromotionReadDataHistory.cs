namespace Campaign.Shared.DataBaseContext.Entities.Product
{
    public class ProductPromotionReadDataHistory
    {
        public int Id { get; private set; }
        public int PromotionCode { get; private set; }
        public DateTime ReadAt { get; private set; }

        public ProductPromotionReadDataHistory(int id,
                                               DateTime readAt,
                                               int promotionCode)
        {
            Id = id;
            ReadAt = readAt;
            PromotionCode = promotionCode;
        }
    }
}
