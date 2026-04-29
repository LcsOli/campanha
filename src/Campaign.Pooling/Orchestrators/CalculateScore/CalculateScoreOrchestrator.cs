using Campaign.Pooling.Commands.Calculate;
using Campaign.Pooling.Commands.Consumers.Get;
using Campaign.Pooling.Handlers.CalculateCoupons;
using Campaign.Pooling.Commands.SellerManager.Update;
using Campaign.Pooling.Handlers.CalculateRevenueTarget;
using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.SellerScore.GetSellersScore;
using Campaign.Pooling.Handlers.CalculateRegisteredsConsummers;
using Campaign.Pooling.Handlers.CalculateReactivatedsConsummers;
using Campaign.Pooling.Handlers.UpdateCurrentRevenueSellerManager;

namespace Campaign.Pooling.Orchestrators.UpdateSellerScore
{
    public class CalculateScoreOrchestrator : ICalculateScoreOrchestrator
    {
        private readonly ILogger<CalculateScoreOrchestrator> _logger;

        private readonly IGetSellerScoreHandler _getSellerScoreHandler;
        private readonly ICalculateRevenueHandler _calculateRevenueHandler;
        private readonly ICalculateCouponsHandler _calculateCouponsHandler;
        private readonly ICalculateScoreByProductHandler _calculateScorePointsByProductHandler;
        private readonly ICalculateRegisteredsConsumersHandler _calculateRegisteredsConsumersHandler;
        private readonly ICalculateReactivatedsConsumersHandler _calculateReactivatedsConsumersHandler;
        private readonly IUpdateCurrentRevenueSellerManagerScoreHandler _updateCurrentRevenueSellerManagerHandler;

        public CalculateScoreOrchestrator(ILogger<CalculateScoreOrchestrator> logger,
                                          IGetSellerScoreHandler getSellerScoreHandler,
                                          ICalculateRevenueHandler calculateRevenueHandler,
                                          ICalculateCouponsHandler calculateCouponsHandler,
                                          ICalculateScoreByProductHandler calculateScorePointsByProductHandler,
                                          ICalculateRegisteredsConsumersHandler calculateRegisteredsConsumersHandler,
                                          ICalculateReactivatedsConsumersHandler calculateReactivatedsConsumersHandler,
                                          IUpdateCurrentRevenueSellerManagerScoreHandler updateCurrentRevenueSellerManagerHandler)
        {
            _logger = logger;
            _getSellerScoreHandler = getSellerScoreHandler;
            _calculateCouponsHandler = calculateCouponsHandler;
            _calculateRevenueHandler = calculateRevenueHandler;
            _calculateRegisteredsConsumersHandler = calculateRegisteredsConsumersHandler;
            _calculateScorePointsByProductHandler = calculateScorePointsByProductHandler;
            _calculateReactivatedsConsumersHandler = calculateReactivatedsConsumersHandler;
            _updateCurrentRevenueSellerManagerHandler = updateCurrentRevenueSellerManagerHandler;
        }

        public async Task Execute(int promotionCode)
        {
            _logger.LogWarning("Getting Sellers Scores.");
            var sellersScore = await _getSellerScoreHandler.Handle();

            _logger.LogWarning("Calculating points by products.");
            await _calculateScorePointsByProductHandler.Handle(new CalculateScoreByProductCommand(promotionCode, sellersScore));

            _logger.LogCritical("Calculating reactivateds cosummers.");
            await _calculateReactivatedsConsumersHandler.Handle(new CalculateReactivatedsConsumersCommand(promotionCode, sellersScore));

            _logger.LogWarning("Calculating registereds cosummers.");
            await _calculateRegisteredsConsumersHandler.Handler(new CalculateRegisteredsConsumersCommand(promotionCode, sellersScore));

            _logger.LogWarning("Calculating revenue.");
            await _calculateRevenueHandler.Handle(new CalculateRevenueCommand(promotionCode, sellersScore));

            _logger.LogWarning("Calculating revenue of month.");
            await _calculateRevenueHandler.Handle(new CalculateRevenueMonthCommand(promotionCode, sellersScore));
            
            _logger.LogWarning("Calculating seller manager revenue.");
            await _updateCurrentRevenueSellerManagerHandler.Handle(new UpdateCurrentRevenueSellerManagerCommand(sellersScore));

            _logger.LogWarning("Calculating coupons.");
            _calculateCouponsHandler.Handle(new CalculateCouponsCommand(sellersScore));
        }
    }
}