using Campaign.Pooling.DTO.Response.Order;

namespace Campaign.Processor.API.DTO.Response.Order
{
    public record OrderDetailPromotionResponse : OrderDetailResponse
    {
        public DateTime PromotionProcessedIn { get; init; }
        public OrderDetailPromotionResponse(long OrderId,
                                            int SellerId,
                                            int ProductId,
                                            int ConsumerId,
                                            decimal Quantity,
                                            DateTime DateOfSale,
                                            string ConsumerName,
                                            DateTime? CanceledIn,
                                            string ProductDescription,
                                            DateTime PromotionProcessedIn,
                                            decimal? ProductPromotionPoints) : base(OrderId,
                                                                                    SellerId,
                                                                                    ProductId,
                                                                                    ConsumerId,
                                                                                    Quantity,
                                                                                    DateOfSale,
                                                                                    ConsumerName,
                                                                                    CanceledIn,
                                                                                    ProductDescription,
                                                                                    ProductPromotionPoints)
        {
            this.PromotionProcessedIn = PromotionProcessedIn;
        }
    }
}
