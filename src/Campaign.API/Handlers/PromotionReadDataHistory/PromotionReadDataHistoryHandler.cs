using Campaign.API.Commands.PromotionReadDataHistory.Get;
using Campaign.API.DTO.PromotionReadDataHistory.Response;
using Campaign.API.Repositories.PromotionReadDataHistory.ReadOnly;

namespace Campaign.API.Handlers.PromotionReadDataHistory
{
    public class PromotionReadDataHistoryHandler : IPromotionReadDataHistoryHandler
    {
        private readonly IPromotionReadDataHistoryRepositorie _promotionReadDataHistoryRepositorie;
        public PromotionReadDataHistoryHandler(IPromotionReadDataHistoryRepositorie promotionReadDataHistoryRepositorie)
        {
            _promotionReadDataHistoryRepositorie = promotionReadDataHistoryRepositorie;
        }

        public async Task<List<PromotionReadResponse>> Handle(GetAllReadHistoryCommand cmd)
        {
            var promotionsRead = await _promotionReadDataHistoryRepositorie.GetAll();
            return [.. promotionsRead.Select(x => new PromotionReadResponse(x.Id, x.InitIn, x.EndIn))];
        }
    }
}
