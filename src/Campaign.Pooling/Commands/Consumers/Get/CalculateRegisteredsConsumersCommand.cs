using Campaign.Processor.API.Commands.Summaries.Create;
using Campaign.Processor.API.DTO.Response.Seller;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Commands.Consumers.Get
{
    public record CalculateRegisteredsConsumersCommand(int PromotionCode,
                                                      List<Entity.SellerScore> SellersScores,
                                                      List<RegisteredsConsumerResponse> RegisteredsConsumers) : CustomerSalesEventPointsCommand;
}
