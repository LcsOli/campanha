using Campaign.Pooling.Commands.Consumers.Get;
using Campaign.Processor.API.Commands.Summaries.Create;
using Campaign.Pooling.Repositories.OrderSummary.ReadOnly;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;
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

        public CalculateScoreByCustomerSalesEventOrchestrator(IOrderSummaryReadOnlyRepository orderSummaryReadOnlyRepository,
                                                              ICalculateReactivatedsConsumersHandler calculateReactivatedsConsumersHandler,
                                                              IRegisterSellerScoreClientSummaryHandler registerSellerScoreClientSummaryHandler)
        {
            _orderSummaryReadOnlyRepository = orderSummaryReadOnlyRepository;
            _calculateReactivatedsConsumersHandler = calculateReactivatedsConsumersHandler;
            _registerSellerScoreClientSummaryHandler = registerSellerScoreClientSummaryHandler;
        }

        public async Task Execute(int promotionCode, List<Entity.SellerScore> SellersScores)
        {
            var reactivateds = await _orderSummaryReadOnlyRepository.GetCustomersReactivatedsBySelller(promotionCode);

            await _calculateReactivatedsConsumersHandler.Handle(new CalculateReactivatedsConsumersCommand(promotionCode, reactivateds, SellersScores));

            await _registerSellerScoreClientSummaryHandler.Handle(new RegisterReactivatedsCommand(promotionCode, SellersScores, reactivateds));
        }
    }
}
