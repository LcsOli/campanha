using Campaign.Pooling.Commands.Orders.Get;
using Campaign.Pooling.Commands.Consumers.Get;
using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.OrderDetail.GetOrdersDetail;
using Campaign.Pooling.Handlers.SellerScore.GetSellersScore;
using Campaign.Pooling.Handlers.CalculatePositivatedsConsummers;
using Campaign.Pooling.Handlers.CalculateReactivatedsConsummers;

namespace Campaign.Pooling.Orchestrators.UpdateSellerScore
{
    public class CalcuateScoreOrchestrator : ICalcuateScoreOrchestrator
    {
        private readonly IGetSellerScoreHandler _getSellerScoreHandler;
        private readonly IGetOrdersDetailHandler _getOrdersDetailHandler;
        private readonly ICalculateScoreByProductHandler _calculateScorePointsByProductHandler;
        private readonly ICalculateRegisteredsConsumersHandler _calculateRegisteredsConsumersHandler;
        private readonly ICalculateReactivatedsConsumersHandler _calculateReactivatedsConsumersHandler;

        public CalcuateScoreOrchestrator(IGetSellerScoreHandler getSellerScoreHandler,
                                         IGetOrdersDetailHandler getOrdersDetailHandler,
                                         ICalculateScoreByProductHandler calculateScorePointsByProductHandler,
                                         ICalculateReactivatedsConsumersHandler calculateReactivatedsConsumersHandler,
                                         ICalculateRegisteredsConsumersHandler calculateRegisteredsConsumersHandler)
        {
            _getSellerScoreHandler = getSellerScoreHandler;
            _getOrdersDetailHandler = getOrdersDetailHandler;
            _calculateRegisteredsConsumersHandler = calculateRegisteredsConsumersHandler;
            _calculateScorePointsByProductHandler = calculateScorePointsByProductHandler;
            _calculateReactivatedsConsumersHandler = calculateReactivatedsConsumersHandler;
        }

        public async Task Execute(int promotionCode, DateTime dtWeekToStartProcess, DateTime dtWeekToStopProcess)
        {
            var ordersDetail = await _getOrdersDetailHandler.Handle(new GetOrdersDetailCommand(promotionCode, dtWeekToStartProcess, dtWeekToStopProcess));
            var sellersScore = await _getSellerScoreHandler.Handle();

            _calculateScorePointsByProductHandler.Handle(new CalculateScoreByProductCommand(ordersDetail, sellersScore));

            var consumersIds = ordersDetail.Select(x => x.ConsumerId).ToHashSet();

            //TODO - Verificar a possibilidade de armazenar dados em cache para evitar que varios parametros
            //sejam passados para o handler de calculo de reativados e registrados, visto que ambos precisam dos mesmos parametros.

            await _calculateReactivatedsConsumersHandler.Handle(new CalculateReactivatedsConsumersCommand(promotionCode,
                                                                                                          [.. consumersIds],
                                                                                                          SellersScores: sellersScore,
                                                                                                          DtWeekToStopProcess: dtWeekToStopProcess,
                                                                                                          DtWeekToStartProcess: dtWeekToStartProcess));

            await _calculateRegisteredsConsumersHandler.Handler(new CalculateRegisteredsConsumersCommand(promotionCode,
                                                                                                         [.. consumersIds],
                                                                                                         SellersScores: sellersScore,
                                                                                                         DtWeekToStopProcess: dtWeekToStopProcess,
                                                                                                         DtWeekToStartProcess: dtWeekToStartProcess));
        }
    }
}
