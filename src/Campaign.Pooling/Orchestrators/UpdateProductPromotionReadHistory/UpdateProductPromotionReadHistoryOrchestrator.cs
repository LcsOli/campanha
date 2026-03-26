using Campaign.Shared.DataBaseContext.Entities.UnityOfWork;
using Campaign.Pooling.Commands.ProductPromotionReadHistory.Get;
using Campaign.Pooling.Commands.ProductPromotionReadHistory.Create;
using Campaign.Pooling.Orchestrators.UpdateProductPromotionReadHistory.Validator;
using Campaign.Pooling.Handlers.ProductPromotionReadDataHistory.RegisterNewHistory;
using Campaign.Pooling.Handlers.ProductPromotionHistory.GetLastProductPromotionsHistory;

namespace Campaign.Pooling.Orchestrators.UpdateProductPromotionReadHistory
{
    public class UpdateProductPromotionReadHistoryOrchestrator : IUpdateProductPromotionReadHistoryOrchestrator
    {
        private readonly IUnityOfWork _unityOfWork;

        private readonly IGetProductPromotionReadDataHistoryHandler _getProductPromotionReadDataHistoryHandler;
        private readonly IRegisterProductPromotionReadDataHistoryHandler _registerProductPromotionReadDataHistoryHandler;

        public UpdateProductPromotionReadHistoryOrchestrator(IUnityOfWork unityOfWork,
                                                             IGetProductPromotionReadDataHistoryHandler getProductPromotionReadDataHistoryHandler,
                                                             IRegisterProductPromotionReadDataHistoryHandler registerProductPromotionReadDataHistoryHandler)
        {
            _unityOfWork = unityOfWork;
            _getProductPromotionReadDataHistoryHandler = getProductPromotionReadDataHistoryHandler;
            _registerProductPromotionReadDataHistoryHandler = registerProductPromotionReadDataHistoryHandler;
        }

        public async Task Execute(int promotionCode)
        {
            var productPromotionReadHistory = await _getProductPromotionReadDataHistoryHandler.Handle(new GetProductPromotionReadHistoryCommand(promotionCode));

            new ProductPromotionExistsValidator()
                 .Validate(productPromotionReadHistory!);

            await _registerProductPromotionReadDataHistoryHandler.Handle(new RegisterProductPromotionReadDataHistoryCommand(promotionCode));

            await _unityOfWork.SaveAsync();
        }
    }
}
