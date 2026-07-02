using Campaign.Shared.Enums.SellerScoreConsumerType;

namespace Campaign.API.Commands.SellerScore.Get
{
    public record GetSellerScoreClientSummaryCommand(int Size,
                                                     int Page,
                                                     int SellerId,
                                                     int? CustomerId,
                                                     int PromotionCode,
                                                     CustomerSalesEventType? CustomerSalesEventType);
}
