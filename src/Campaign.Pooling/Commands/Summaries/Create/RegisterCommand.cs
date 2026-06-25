using Campaign.Pooling.DTO.Response.Seller;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Processor.API.Commands.Summaries.Create
{
    public record RegisterCommand(int PromotionCode,
                                  List<Entity.SellerScore> SellersScores) : CustomerSalesEventPointsCommand;
}
