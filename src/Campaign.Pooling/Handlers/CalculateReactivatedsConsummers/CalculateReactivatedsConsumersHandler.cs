using Campaign.Pooling.Commands.Consumers.Get;
using Campaign.Pooling.Repositories.OrderSummary.ReadOnly;

namespace Campaign.Pooling.Handlers.CalculateReactivatedsConsummers
{
    public class CalculateReactivatedsConsumersHandler : ICalculateReactivatedsConsumersHandler
    {
        private readonly int _pointsToAdd = 1_000;

        private readonly IOrderSummaryReadOnlyRepository _orderSummaryReadOnlyRepository;
        public CalculateReactivatedsConsumersHandler(IOrderSummaryReadOnlyRepository orderSummaryReadOnlyRepository)
        {
            _orderSummaryReadOnlyRepository = orderSummaryReadOnlyRepository;
        }

        public async Task Handle(CalculateReactivatedsConsumersCommand cmd)
        {
            var clientsReactivateds = await _orderSummaryReadOnlyRepository.GetCustomersReactivatedsBySelller(cmd.PromotionCode);

            cmd.SellersScores.ForEach(sellerScore =>
            {
                var reactivateds = clientsReactivateds.Where(x => x.SellerId == sellerScore.SellerId);

                sellerScore?.UpdateScore(reactivateds.Count() * _pointsToAdd);
                sellerScore?.UpdateQtyReactivateds((short)reactivateds.Count());
            });
        }
    }
}
