using Campaign.Pooling.Commands.Calculate;
using Campaign.Pooling.Commands.Consumers.Get;
using Campaign.Pooling.Handlers.CalculateCoupons;
using Campaign.Pooling.Commands.SellerManager.Update;
using Campaign.Pooling.Handlers.CalculateRevenueTarget;
using Campaign.Pooling.Handlers.SellerScore.GetSellersScore;
using Campaign.Pooling.Handlers.CalculateRegisteredsConsummers;
using Campaign.Pooling.Handlers.CalculateReactivatedsConsummers;
using Campaign.Pooling.Handlers.UpdateCurrentRevenueSellerManager;
using Campaign.Processor.API.Orchestrators.CalculateScoreByProduct;
using Campaign.Processor.API.Orchestrators.CalculateScoreByCustomerSalesEvent;
using Campaign.Processor.API.Commands.Consumers.Get;

namespace Campaign.Pooling.Orchestrators.UpdateSellerScore
{
    public class CalculateScoreOrchestrator : ICalculateScoreOrchestrator
    {
        private readonly ILogger<CalculateScoreOrchestrator> _logger;

        private readonly ICalculateScoreByProductOrchestrator _calculateScoreByProductOrchestrator;
        private readonly ICalculateScoreByCustomerSalesEventOrchestrator _calculateScoreByCustomerSalesEventOrchestrator;

        private readonly IGetSellerScoreHandler _getSellerScoreHandler;
        private readonly ICalculateRevenueHandler _calculateRevenueHandler;
        private readonly ICalculateCouponsHandler _calculateCouponsHandler;
        private readonly ICalculateRegisteredsConsumersHandler _calculateRegisteredsConsumersHandler;
        private readonly ICalculateReactivatedsConsumersHandler _calculateReactivatedsConsumersHandler;
        private readonly IUpdateCurrentRevenueSellerManagerScoreHandler _updateCurrentRevenueSellerManagerHandler;


        public CalculateScoreOrchestrator(ILogger<CalculateScoreOrchestrator> logger,
                                          IGetSellerScoreHandler getSellerScoreHandler,
                                          ICalculateRevenueHandler calculateRevenueHandler,
                                          ICalculateCouponsHandler calculateCouponsHandler,
                                          ICalculateScoreByProductOrchestrator calculateScoreByProductOrchestrator,
                                          ICalculateRegisteredsConsumersHandler calculateRegisteredsConsumersHandler,
                                          ICalculateReactivatedsConsumersHandler calculateReactivatedsConsumersHandler,
                                          IUpdateCurrentRevenueSellerManagerScoreHandler updateCurrentRevenueSellerManagerHandler,
                                          ICalculateScoreByCustomerSalesEventOrchestrator calculateScoreByCustomerSalesEventOrchestrator)
        {
            _logger = logger;

            _calculateScoreByProductOrchestrator = calculateScoreByProductOrchestrator;
            _calculateScoreByCustomerSalesEventOrchestrator = calculateScoreByCustomerSalesEventOrchestrator;

            _getSellerScoreHandler = getSellerScoreHandler;
            _calculateCouponsHandler = calculateCouponsHandler;
            _calculateRevenueHandler = calculateRevenueHandler;
            _calculateRegisteredsConsumersHandler = calculateRegisteredsConsumersHandler;
            _calculateReactivatedsConsumersHandler = calculateReactivatedsConsumersHandler;
            _updateCurrentRevenueSellerManagerHandler = updateCurrentRevenueSellerManagerHandler;
        }

        public async Task Execute(int promotionCode)
        {
            _logger.LogWarning("Getting Sellers Scores.");
            var sellersScore = await _getSellerScoreHandler.Handle();

            _logger.LogWarning("Calculating points by products.");
            await _calculateScoreByProductOrchestrator.Execute(promotionCode, sellersScore);

            _logger.LogCritical("Calculating reactivateds cosumers.");
            await _calculateScoreByCustomerSalesEventOrchestrator.Execute(new CalculateScoreCustomerReactivatedsSalesEventCommand(promotionCode, sellersScore));

            _logger.LogWarning("Calculating registereds cosumers.");
            await _calculateScoreByCustomerSalesEventOrchestrator.Execute(new CalculateScoreCustomerRegisteredsSalesEventCommand(promotionCode, sellersScore));

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