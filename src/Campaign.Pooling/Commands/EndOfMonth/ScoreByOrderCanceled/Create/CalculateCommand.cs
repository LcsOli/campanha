using Campaign.Processor.API.DTO.Response.Order;

namespace Campaign.Processor.API.Commands.EndOfMonth.ScoreByOrderCanceled.Create
{
    public record CalculateCommand(int PromotionCode);
}
