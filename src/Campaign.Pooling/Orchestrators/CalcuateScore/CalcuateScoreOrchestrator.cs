using Campaign.Pooling.Commands.Orders.Get;
using Campaign.Pooling.Commands.Consumers.Get;
using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.OrderDetail.GetOrdersDetail;
using Campaign.Pooling.Handlers.SellerScore.GetSellersScore;
using Campaign.Pooling.Handlers.CalculateReactivatedsConsummers;

namespace Campaign.Pooling.Orchestrators.UpdateSellerScore
{
    public class CalcuateScoreOrchestrator : ICalcuateScoreOrchestrator
    {
        private readonly IGetSellerScoreHandler _getSellerScoreHandler;
        private readonly IGetOrdersDetailHandler _getOrdersDetailHandler;
        private readonly ICalculateScoreByProductHandler _calculateScorePointsByProductHandler;
        private readonly ICalculateReactivatedsConsumersHandler _calculateReactivatedsConsumersHandler;

        public CalcuateScoreOrchestrator(IGetSellerScoreHandler getSellerScoreHandler,
                                         IGetOrdersDetailHandler getOrdersDetailHandler,
                                         ICalculateScoreByProductHandler calculateScorePointsByProductHandler,
                                         ICalculateReactivatedsConsumersHandler calculateReactivatedsConsumersHandler)
        {
            _getSellerScoreHandler = getSellerScoreHandler;
            _getOrdersDetailHandler = getOrdersDetailHandler;
            _calculateScorePointsByProductHandler = calculateScorePointsByProductHandler;
            _calculateReactivatedsConsumersHandler = calculateReactivatedsConsumersHandler;
        }

        public async Task Execute(int promotionCode, DateTime initIn, DateTime endIn)
        {
            var ordersDetail = await _getOrdersDetailHandler.Handle(new GetOrdersDetailCommand(promotionCode, initIn, endIn));
            var sellersScore = await _getSellerScoreHandler.Handle();

            _calculateScorePointsByProductHandler.Handle(new CalculateScoreByProductCommand(ordersDetail, sellersScore));

            var consumersIds = ordersDetail.Select(x => x.ConsumerId).ToHashSet();

            await _calculateReactivatedsConsumersHandler.Handle(new CalculateReactivatedsConsumersCommand([.. consumersIds], 
                                                                                                          SellersScores: sellersScore, 
                                                                                                          CutoffDate: new DateTime(2026, 01, 01)));
        }
    }
}
