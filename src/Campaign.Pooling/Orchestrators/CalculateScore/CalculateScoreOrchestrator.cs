using Campaign.Pooling.Commands.Calculate;
using Campaign.Pooling.Commands.Consumers.Get;
using Campaign.Pooling.Handlers.CalculateCoupons;
using Campaign.Pooling.Handlers.CalculateRevenueTarget;
using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.SellerScore.GetSellersScore;
using Campaign.Pooling.Handlers.CalculateRegisteredsConsummers;
using Campaign.Pooling.Handlers.CalculateReactivatedsConsummers;

namespace Campaign.Pooling.Orchestrators.UpdateSellerScore
{
    public class CalculateScoreOrchestrator : ICalculateScoreOrchestrator
    {
        private readonly IGetSellerScoreHandler _getSellerScoreHandler;
        private readonly ICalculateRevenueHandler _calculateRevenueHandler;
        private readonly ICalculateCouponsHandler _calculateCouponsHandler;
        private readonly ICalculateScoreByProductHandler _calculateScorePointsByProductHandler;
        private readonly ICalculateRegisteredsConsumersHandler _calculateRegisteredsConsumersHandler;
        private readonly ICalculateReactivatedsConsumersHandler _calculateReactivatedsConsumersHandler;

        public CalculateScoreOrchestrator(IGetSellerScoreHandler getSellerScoreHandler,
                                         ICalculateRevenueHandler calculateRevenueHandler,
                                         ICalculateCouponsHandler calculateCouponsHandler,
                                         ICalculateScoreByProductHandler calculateScorePointsByProductHandler,
                                         ICalculateRegisteredsConsumersHandler calculateRegisteredsConsumersHandler,
                                         ICalculateReactivatedsConsumersHandler calculateReactivatedsConsumersHandler)
        {
            _getSellerScoreHandler = getSellerScoreHandler;
            _calculateCouponsHandler = calculateCouponsHandler;
            _calculateRevenueHandler = calculateRevenueHandler;
            _calculateRegisteredsConsumersHandler = calculateRegisteredsConsumersHandler;
            _calculateScorePointsByProductHandler = calculateScorePointsByProductHandler;
            _calculateReactivatedsConsumersHandler = calculateReactivatedsConsumersHandler;
        }

        public async Task Execute(int promotionCode)
        {
            var sellersScore = await _getSellerScoreHandler.Handle();

            await _calculateScorePointsByProductHandler.Handle(new CalculateScoreByProductCommand(promotionCode, sellersScore));

            await _calculateReactivatedsConsumersHandler.Handle(new CalculateReactivatedsConsumersCommand(promotionCode, sellersScore));
            await _calculateRegisteredsConsumersHandler.Handler(new CalculateRegisteredsConsumersCommand(promotionCode, sellersScore));

            await _calculateRevenueHandler.Handle(new CalculateRevenueCommand(promotionCode, sellersScore));
            await _calculateRevenueHandler.Handle(new CalculateRevenueMonthCommand(promotionCode, sellersScore));

            _calculateCouponsHandler.Handle(new CalculateCouponsCommand(sellersScore));
        }
    }
}
