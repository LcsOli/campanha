using Campaign.Pooling.DTO.Response.Order;

namespace Campaign.Processor.API.Commands.EndOfMonth.ScoreByOrderCanceled.Create
{
    public record CalculateCommand(List<OrderDetailResponse> Orders);
}
