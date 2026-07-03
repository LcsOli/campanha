using Campaign.Pooling.DTO.Response.Order;

namespace Campaign.Processor.API.Commands.ScoreRemoved.Create
{
    public record RegisterScoreRemovedCommand(int PromotionCode,
                                              List<OrderDetailResponse> OrdersDetails);
}
