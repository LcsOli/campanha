using Campaign.Pooling.Commands.Consumers.Get;
using Campaign.Pooling.Repositories.OrderSummary.ReadOnly;

namespace Campaign.Pooling.Handlers.CalculateReactivatedsConsummers
{
    public class CalculateReactivatedsConsumersHandler : ICalculateReactivatedsConsumersHandler
    {

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

                var qty = reactivateds.Count();

                if (qty <= 0) return;

                sellerScore.UpdateScore(qty * cmd.Points);
                sellerScore.UpdateQtyReactivateds((short)qty);
            });
        }
    }
}
