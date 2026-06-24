using Campaign.Processor.API.DTO.Response.Seller;
using Campaign.Processor.API.Commands.Summaries.Create;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Commands.Consumers.Get
{
    public record CalculateReactivatedsConsumersCommand(int PromotionCode,
                                                        List<ReactivatedsConsumerResponse> ReactivatedsConsumers,
                                                        List<Entity.SellerScore> SellersScores): CustomerSalesEventPointsCommand;
}
