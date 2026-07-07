namespace Campaign.Pooling.DTO.Response.Order
{
    public record OrderDetailResponse(int OrderId,
                                      int SellerId,
                                      int ProductId,
                                      int ConsumerId,
                                      decimal Quantity,
                                      DateTime DateOfSale,
                                      string ConsumerName,
                                      DateTime? CanceledIn,
                                      string ProductDescription,
                                      decimal? ProductPromotionPoints);
}
