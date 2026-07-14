using Campaign.Pooling.Orchestrators.UpdateSellerScore;
using Campaign.Shared.DataBaseContext.Entities.UnityOfWork;
using Campaign.Processor.API.Orchestrators.CalculateEndOfMonth;
using Campaign.Pooling.Orchestrators.UpdateProductPromotionReadHistory;

namespace Campaign.Pooling.Orchestrators.MainOrchestrator
{
    public class MainOrchestrator : IMainOrchestrator
    {
        private readonly IUnityOfWork _unityOfWork;

        private readonly ICalculateScoreOrchestrator _calculateScoreOrchestrator;
        private readonly ICalculateEndOfMonthOrchestrator _calculateEndOfMonthOrchestrator;
        private readonly IUpdateProductPromotionReadHistoryOrchestrator _updateProductPromotionReadHistoryOrchestrator;

        public MainOrchestrator(IUnityOfWork unityOfWork,
                                ICalculateScoreOrchestrator calculateScoreOrchestrator,
                                ICalculateEndOfMonthOrchestrator calculateEndOfMonthOrchestrator,
                                IUpdateProductPromotionReadHistoryOrchestrator updateProductPromotionReadHistoryOrchestrator)
        {
            _unityOfWork = unityOfWork;
            _calculateScoreOrchestrator = calculateScoreOrchestrator;
            _calculateEndOfMonthOrchestrator = calculateEndOfMonthOrchestrator;
            _updateProductPromotionReadHistoryOrchestrator = updateProductPromotionReadHistoryOrchestrator;
        }

        public async Task Execute(int promotionCode)
        {
            await _unityOfWork.SecureCommitAsync(async () =>
            {
                await _updateProductPromotionReadHistoryOrchestrator.Execute(promotionCode);
                await _calculateScoreOrchestrator.Execute(promotionCode);

                await _unityOfWork.SaveAsync();

                await _calculateEndOfMonthOrchestrator.Execute(promotionCode);
                await _unityOfWork.SaveAsync();
            });
        }
    }
}