using Campaign.Pooling.Commands.Orders.Get;
using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.SellerScore.GetSellersScore;
using Campaign.Pooling.Handlers.OrderDetail.GetOrdersDetail;

namespace Campaign.Pooling.Orchestrators.UpdateSellerScore
{
    public class CalcuateScoreByProductOrchestrator : ICalcuateScoreByProductOrchestrator
    {
        private readonly IGetSellerScoreHandler _getSellerScoreHandler;
        private readonly IGetOrdersDetailHandler _getOrdersDetailHandler;
        private readonly ICalculateScorePointsHandler _calculateScorePointsHandler;

        public CalcuateScoreByProductOrchestrator(IGetSellerScoreHandler getSellerScoreHandler,
                                                  IGetOrdersDetailHandler getOrdersDetailHandler,
                                                  ICalculateScorePointsHandler calculateScorePointsHandler)
        {
            _getSellerScoreHandler = getSellerScoreHandler;
            _getOrdersDetailHandler = getOrdersDetailHandler;
            _calculateScorePointsHandler = calculateScorePointsHandler;
        }

        public async Task Execute(int promotionCode, DateTime initIn, DateTime endIn)
        {
            var ordersDetail = await _getOrdersDetailHandler.Handle(new GetOrdersDetailCommand(promotionCode, initIn, endIn));
            var sellersScore = await _getSellerScoreHandler.Handle();

            _calculateScorePointsHandler.Handle(new CalculateScoreByProductCommand(ordersDetail, sellersScore));
        }
    }
}
