namespace Campaign.API.Commands.SellerScore.Get
{
    public record GetSellerScoreProductSummaryCommand(int Size,
                                                      int Page,
                                                      int SellerId,
                                                      int? ProductId,
                                                      int? Customer,
                                                      int PromotionCode);
}
