using Campaign.API.Commands.PromotionReadDataHistory.Get;
using Campaign.API.DTO.PromotionReadDataHistory.Response;

namespace Campaign.API.Handlers.PromotionReadDataHistory
{
    public interface IPromotionReadDataHistoryHandler
    {
        Task<List<PromotionReadResponse>> Handle(GetAllReadHistoryCommand cmd);
    }
}
