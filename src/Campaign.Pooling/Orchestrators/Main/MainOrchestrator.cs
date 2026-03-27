using Campaign.Pooling.Orchestrators.UpdateProductPromotionReadHistory;
using Campaign.Shared.DataBaseContext.Entities.UnityOfWork;

namespace Campaign.Pooling.Orchestrators.MainOrchestrator
{
    public class MainOrchestrator : IMainOrchestrator
    {
        private readonly IUnityOfWork _unityOfWork;

        private readonly IUpdateProductPromotionReadHistoryOrchestrator _updateProductPromotionReadHistoryOrchestrator;
        public MainOrchestrator(IUnityOfWork unityOfWork,
                                IUpdateProductPromotionReadHistoryOrchestrator updateProductPromotionReadHistoryOrchestrator)
        {
            _unityOfWork = unityOfWork;
            _updateProductPromotionReadHistoryOrchestrator = updateProductPromotionReadHistoryOrchestrator;
        }

        public async Task Execute(int promotionCode, DateTime initIn, DateTime endIn)
        {
            _unityOfWork.SecureCommitAsync(async () =>
            {
                await _updateProductPromotionReadHistoryOrchestrator.Execute(promotionCode);

            });
        }
    }
}
