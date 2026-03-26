using System.Net;
using Campaign.Shared.Exceptions;
using Campaign.Pooling.Commands.CalculateScore;
using Campaign.Pooling.Commands.ProductPromotionReadHistory.Get;
using Campaign.Pooling.Handlers.CalculateScore.Validator;
using Campaign.Pooling.Handlers.ProductPromotionHistory.GetLastProductPromotionsHistory;

namespace Campaign.Pooling.Handlers.CalculateScore
{
    public class CalculateScoreOrchestrator : ICalculateScoreOrchestrator
    {
        private readonly IGetProductPromotionReadDataHistoryHandler _getProductPromotionReadDataHistoryHandler;

        public CalculateScoreOrchestrator(IGetProductPromotionReadDataHistoryHandler getProductPromotionReadDataHistoryHandler)
        {
            _getProductPromotionReadDataHistoryHandler = getProductPromotionReadDataHistoryHandler;
        }

        public async Task Handle(CalculateScoreCommand cmd)
        {
            new PromotionCodeValidator().Validate(cmd);

            var productPromotionalReaddataHistory = await _getProductPromotionReadDataHistoryHandler.Handle(new GetProductPromotionReadHistoryCommand(cmd.PromotionCode));

            if (productPromotionalReaddataHistory != null)
                throw new CompaignException(HttpStatusCode.Forbidden, $"Esta promoção já foi calculada.");



        }
    }
}
