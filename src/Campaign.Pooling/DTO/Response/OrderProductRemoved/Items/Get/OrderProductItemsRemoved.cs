namespace Campaign.Processor.API.DTO.Response.OrderProductRemoved.Items.Get
{
    public record OrderProductItemsRemoved(long OrderId, int SellerId, int ProductId, int ConsumerId, decimal? ProductPromotionPoints);
}
