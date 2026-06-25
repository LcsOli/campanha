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

        public async Task Handle(RegisterReactivatedsCommand cmd)
        {
            var summaries = new List<SellerScoreClientsSummary>();

            cmd.SellersScores.ForEach(sellerScore =>
            {
                var Reactivateds = cmd.ReactivatedsConsumers.Where(x => x.SellerId == sellerScore.SellerId);

                var qty = Reactivateds.Count();

                if (qty <= 0) return;

                summaries.AddRange(Reactivateds.Select(x => new SellerScoreClientsSummary(points: cmd.Points,
                                                                                          sellerId: x.SellerId,
                                                                                          customerId: x.CustomerId,
                                                                                          registeredIn: x.RegisteredIn,
                                                                                          reactivatedIn: x.ReactivatedIn,
                                                                                          promotionCode: cmd.PromotionCode,
                                                                                          customerSalesEventType: CustomerSalesEventType.Reactivated)));
            });

            await _sellerScoreClientSummaryRepository.AddRange(summaries);
        }

        public async Task Handle(RegisterRegisteredsCommand cmd)
        {
            var summaries = new List<SellerScoreClientsSummary>();

            cmd.SellersScores.ForEach(sellerScore =>
            {
                var registereds = cmd.RegisteredsConsumers.Where(x => x.SellerId == sellerScore.SellerId);

                var qty = registereds.Count();

                if (qty <= 0) return;

                summaries.AddRange(registereds.Select(x => new SellerScoreClientsSummary(points: cmd.Points,
                                                                                         sellerId: x.SellerId,
                                                                                         customerId: x.CustomerId,
                                                                                         registeredIn: x.RegisteredIn,
                                                                                         promotionCode: cmd.PromotionCode,
                                                                                         customerSalesEventType: CustomerSalesEventType.Registered)));
            });

            await _sellerScoreClientSummaryRepository.AddRange(summaries);
        }
    }
}
