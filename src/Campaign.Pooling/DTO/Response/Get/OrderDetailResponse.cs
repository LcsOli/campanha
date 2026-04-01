namespace Campaign.Pooling.DTO.Response.Get
{
    public record OrderDetailResponse(int SellerId,
                                      decimal Price,
                                      int ProductId,
                                      int ConsumerId,
                                      decimal Quantity,
                                      DateTime DateOfSale,
                                      decimal? ProductPromotionPoints);
}
