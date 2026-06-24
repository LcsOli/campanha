using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

using Campaign.Processor.API.DTO.Response.Seller;

namespace Campaign.Processor.API.Commands.Summaries.Create
{
    public record ReactivatedsCommand(int PromotionCode,
                                      List<Entity.SellerScore> SellersScores,
                                      List<ReactivatedsConsumerResponse> ReactivatedsConsumers) : CustomerSalesEventPointsCommand;
}
