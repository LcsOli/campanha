using Campaign.Shared.Enums.SellerScoreConsumerType;
using Campaign.Processor.API.Commands.Summaries.Create;
using Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;
using Campaign.Processor.API.Repositories.SellerScoreClientSummary.WriteOnly;

namespace Campaign.Processor.API.Handlers.RegisterSellerScoreClientSummary
{
    public class RegisterSellerScoreClientSummaryHandler : IRegisterSellerScoreClientSummaryHandler
    {
        private readonly ISellerScoreClientSummaryRepository _sellerScoreClientSummaryRepository;
        public RegisterSellerScoreClientSummaryHandler(ISellerScoreClientSummaryRepository sellerScoreClientSummaryRepository)
        {
            _sellerScoreClientSummaryRepository = sellerScoreClientSummaryRepository;
        }

        public async Task Handle(ReactivatedsCommand cmd)
        {
            var summaries = new List<SellerScoreClientsSummary>();

            cmd.SellersScores.ForEach(sellerScore =>
            {
                var reactivateds = cmd.ReactivatedsConsumers.Where(x => x.SellerId == sellerScore.SellerId);

                var qty = reactivateds.Count();

                if (qty <= 0) return;

                summaries.AddRange(reactivateds.Select(x => new SellerScoreClientsSummary(points: cmd.Points,
                                                                                          sellerId: x.SellerId,
                                                                                          customerId: x.CustomerId,
                                                                                          registeredIn: x.RegisteredIn,
                                                                                          reactivatedIn: x.ReactivatedIn,
                                                                                          promotionCode: cmd.PromotionCode,
                                                                                          customerSalesEventType: CustomerSalesEventType.Reactivated)));

            });

            await _sellerScoreClientSummaryRepository.AddRange(summaries);
        }
    }
}
