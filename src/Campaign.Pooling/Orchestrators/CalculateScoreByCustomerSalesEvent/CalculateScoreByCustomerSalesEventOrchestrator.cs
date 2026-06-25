using Campaign.Pooling.Commands.Consumers.Get;
using Campaign.Processor.API.Commands.Consumers.Get;
using Campaign.Processor.API.Commands.Summaries.Create;
using Campaign.Pooling.Repositories.OrderSummary.ReadOnly;
using Campaign.Pooling.Handlers.CalculateRegisteredsConsummers;
using Campaign.Pooling.Handlers.CalculateReactivatedsConsummers;
using Campaign.Processor.API.Handlers.RegisterSellerScoreClientSummary;

namespace Campaign.Processor.API.Orchestrators.CalculateScoreByCustomerSalesEvent
{
    public class CalculateScoreByCustomerSalesEventOrchestrator : ICalculateScoreByCustomerSalesEventOrchestrator
    {
        //TODO - Orquestradores vão ter a responsabilidade de lidar com repositórios?

        private readonly IOrderSummaryReadOnlyRepository _orderSummaryReadOnlyRepository;

        private readonly ICalculateReactivatedsConsumersHandler _calculateReactivatedsConsumersHandler;
        private readonly IRegisterSellerScoreClientSummaryHandler _registerSellerScoreClientSummaryHandler;

        private readonly ICalculateRegisteredsConsumersHandler _calculateRegisteredsConsumersHandler;

        public CalculateScoreByCustomerSalesEventOrchestrator(IOrderSummaryReadOnlyRepository orderSummaryReadOnlyRepository,
                                                              ICalculateRegisteredsConsumersHandler calculateRegisteredsConsumersHandler,
                                                              ICalculateReactivatedsConsumersHandler calculateReactivatedsConsumersHandler,
                                                              IRegisterSellerScoreClientSummaryHandler registerSellerScoreClientSummaryHandler)
        {
            _orderSummaryReadOnlyRepository = orderSummaryReadOnlyRepository;

            _calculateRegisteredsConsumersHandler = calculateRegisteredsConsumersHandler;
            _calculateReactivatedsConsumersHandler = calculateReactivatedsConsumersHandler;
            _registerSellerScoreClientSummaryHandler = registerSellerScoreClientSummaryHandler;
        }

        public async Task Execute(CalculateScoreCustomerReactivatedsSalesEventCommand cmd)
        {
            var reactivateds = await _orderSummaryReadOnlyRepository.GetCustomersReactivatedsBySelller(cmd.PromotionCode);

            _calculateReactivatedsConsumersHandler.Handle(new CalculateReactivatedsConsumersCommand(cmd.PromotionCode, cmd.SellersScores, reactivateds));

            await _registerSellerScoreClientSummaryHandler.Handle(new RegisterReactivatedsCommand(cmd.PromotionCode, cmd.SellersScores, reactivateds));
        }

        public async Task Execute(CalculateScoreCustomerRegisteredsSalesEventCommand cmd)
        {
            var registereds = await _orderSummaryReadOnlyRepository.GetCustomersRegisteredsBySelller(cmd.PromotionCode);

            _calculateRegisteredsConsumersHandler.Handle(new CalculateRegisteredsConsumersCommand(cmd.PromotionCode, cmd.SellersScores, registereds));

            await _registerSellerScoreClientSummaryHandler.Handle(new RegisterRegisteredsCommand(cmd.PromotionCode, cmd.SellersScores, registereds));
        }
    }
}
