namespace Campaign.API.Commands.SellerScore.Get
{
    public record GetSellerScoreProductSummaryCommand(int Size,
                                                      int Page,
                                                      int SellerId,
                                                      int? CustomerId,
                                                      int? ProductId,
                                                      int PromotionCode);
}
