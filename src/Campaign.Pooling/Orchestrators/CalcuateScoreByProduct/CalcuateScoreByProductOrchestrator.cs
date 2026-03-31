using Campaign.Pooling.Commands.Orders.Get;
using Campaign.Pooling.Commands.ProductPromotions.Get;
using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.SellerScore.GetSellersScore;
using Campaign.Pooling.Handlers.OrderDetail.GetOrdersDetail;
using Campaign.Pooling.Handlers.ProductPromotion.GetProductsPromotions;

namespace Campaign.Pooling.Orchestrators.UpdateSellerScore
{
    public class CalcuateScoreByProductOrchestrator : ICalcuateScoreByProductOrchestrator
    {
        private readonly IGetSellerScoreHandler _getSellerScoreHandler;
        private readonly IGetOrdersDetailHandler _getOrdersDetailHandler;
        private readonly ICalculateScorePointsHandler _calculateScorePointsHandler;
        private readonly IGetProductsPromotionsHandler _getProductsPromotionsHandler;

        public CalcuateScoreByProductOrchestrator(IGetSellerScoreHandler getSellerScoreHandler,
                                                  IGetOrdersDetailHandler getOrdersDetailHandler,
                                                  IGetProductsPromotionsHandler getProductsPromotionsHandler,
                                                  ICalculateScorePointsHandler calculateScorePointsHandler)
        {
            _getSellerScoreHandler = getSellerScoreHandler;
            _getOrdersDetailHandler = getOrdersDetailHandler;
            _calculateScorePointsHandler = calculateScorePointsHandler;
            _getProductsPromotionsHandler = getProductsPromotionsHandler;
        }

        public async Task Execute(int promotionCode, DateTime initIn, DateTime endIn)
        {
            var ordersDetail = await _getOrdersDetailHandler.Handle(new GetOrdersDetailCommand(promotionCode, initIn, endIn));

            var productsPromotions = await _getProductsPromotionsHandler.Handle(new GetProductsPromotionsCommand([.. ordersDetail.Select(o => o.ProductId)] ,promotionCode));

            var sellersScore = await _getSellerScoreHandler.Handle();

            _calculateScorePointsHandler.Handle(new CalculateScoreByProductCommand(ordersDetail, sellersScore, productsPromotions));
        }
    }
}
