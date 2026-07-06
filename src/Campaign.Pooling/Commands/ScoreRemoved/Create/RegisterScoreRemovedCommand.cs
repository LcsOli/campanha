using Campaign.Pooling.DTO.Response.Order;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Processor.API.Commands.ScoreRemoved.Create
{
    public record RegisterScoreRemovedCommand(int promotionCode,
                                              List<OrderDetailResponse> OrdersDetails,
                                              List<Entity.SellerScore> SellersScores);
}
