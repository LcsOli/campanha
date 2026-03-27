using Campaign.Pooling.Commands.ProductPromotions.Get;
using Campaign.Shared.DataBaseContext.Entities.UnityOfWork;
using Campaign.Pooling.Commands.ProductPromotionReadHistory.Get;
using Campaign.Pooling.Commands.ProductPromotionReadHistory.Create;
using Campaign.Pooling.Handlers.ProductPromotion.ProductPromotionExists;
using Campaign.Pooling.Orchestrators.UpdateProductPromotionReadHistory.Validator;
using Campaign.Pooling.Handlers.ProductPromotionReadDataHistory.RegisterNewHistory;
using Campaign.Pooling.Handlers.ProductPromotionHistory.GetLastProductPromotionsHistory;

namespace Campaign.Pooling.Orchestrators.UpdateProductPromotionReadHistory
{
    public class UpdateProductPromotionReadHistoryOrchestrator : IUpdateProductPromotionReadHistoryOrchestrator
    {
        private readonly IUnityOfWork _unityOfWork;

        private readonly IProductPromotionExistsHandler _productPromotionExistsHandler;
        private readonly IGetProductPromotionReadDataHistoryHandler _getProductPromotionReadDataHistoryHandler;
        private readonly IRegisterProductPromotionReadDataHistoryHandler _registerProductPromotionReadDataHistoryHandler;

        public UpdateProductPromotionReadHistoryOrchestrator(IUnityOfWork unityOfWork,
                                                             IProductPromotionExistsHandler productPromotionExistsHandler,
                                                             IGetProductPromotionReadDataHistoryHandler getProductPromotionReadDataHistoryHandler,
                                                             IRegisterProductPromotionReadDataHistoryHandler registerProductPromotionReadDataHistoryHandler)
        {
            _unityOfWork = unityOfWork;
            _productPromotionExistsHandler = productPromotionExistsHandler;
            _getProductPromotionReadDataHistoryHandler = getProductPromotionReadDataHistoryHandler;
            _registerProductPromotionReadDataHistoryHandler = registerProductPromotionReadDataHistoryHandler;
        }

        public async Task Execute(int promotionCode)
        {
            await _productPromotionExistsHandler.Handle(new ProductPromotionExistsCommand(promotionCode));

            var productPromotionReadHistory = await _getProductPromotionReadDataHistoryHandler.Handle(new GetProductPromotionReadHistoryCommand(promotionCode));

            new ProductPromotionExistsValidator()
                 .Validate(productPromotionReadHistory!);

            await _registerProductPromotionReadDataHistoryHandler.Handle(new RegisterProductPromotionReadDataHistoryCommand(promotionCode));

            await _unityOfWork.SaveAsync();
        }
    }
}
