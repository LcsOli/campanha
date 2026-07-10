using Campaign.Processor.API.DTO.Response.Order;

namespace Campaign.Processor.API.Commands.EndOfMonth.ScoreByOrderRemoved.Create
{
    public record CalculateCommand(int PromotionCode, List<OrderDetailPromotionResponse> Orders);
}
