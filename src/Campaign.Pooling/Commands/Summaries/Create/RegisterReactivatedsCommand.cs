using Campaign.Processor.API.DTO.Response.Seller;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Processor.API.Commands.Summaries.Create
{
    public record RegisterReactivatedsCommand(int PromotionCode,
                                              List<Entity.SellerScore> SellersScores,
                                              List<ReactivatedsConsumerResponse> ReactivatedsConsumers) : CustomerSalesEventPointsCommand;
}
