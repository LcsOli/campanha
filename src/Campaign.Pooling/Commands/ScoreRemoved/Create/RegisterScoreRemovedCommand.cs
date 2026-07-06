using Campaign.Pooling.DTO.Response.Order;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Processor.API.Commands.ScoreRemoved.Create
{
    public record RegisterScoreRemovedCommand(int PromotionCode,
                                              List<OrderDetailResponse> OrdersDetails,
                                              List<Entity.SellerScore> SellersScores);
}
