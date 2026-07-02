using Campaign.Processor.API.Commands.Summaries.Create;
using Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;
using Campaign.Processor.API.Repositories.SellerScoreProductsSummary.WriteOnly;

namespace Campaign.Processor.API.Handlers.RegisterSellerScoreProductSummary
{
    public class RegisterSellerScoreProductSummaryHandler : IRegisterSellerScoreProductSummaryHandler
    {
        private readonly ISellerScoreProductSummaryWriteOnlyRepository _sellerScoreProductSummaryRepository;

        public RegisterSellerScoreProductSummaryHandler(ISellerScoreProductSummaryWriteOnlyRepository sellerScoreProductSummaryRepository)
        {
            _sellerScoreProductSummaryRepository = sellerScoreProductSummaryRepository;
        }

        public async Task Handle(RegisterSellerScoreProductSummaryCommand cmd)
        {
            var summaries = new List<SellerScoreProductsSummary>();

            cmd.SellersScores.ForEach(sellerScore =>
            {
                var clients = cmd.OrdersDetails.Where(o => o.SellerId == sellerScore.SellerId)
                                                      .GroupBy(o => o.ConsumerId)
                                                      .Select(o => new
                                                      {
                                                          CustomerId = o.Key,
                                                          Orders = o.DistinctBy(p => p.ProductId).ToList()
                                                      }).ToList();

                if (clients.Count <= 0)
                    return;

                clients.ForEach(client =>
                {
                    var sellerersScoresProductsSummaries = client.Orders.Select(o => new SellerScoreProductsSummary(productId: o.ProductId,
                                                                                                                    customerId: client.CustomerId,
                                                                                                                    sellerId: sellerScore.SellerId,
                                                                                                                    promotionCode: cmd.PromotionCode,
                                                                                                                    score: o.ProductPromotionPoints!.Value));

                    summaries.AddRange(sellerersScoresProductsSummaries);
                });

            });

            await _sellerScoreProductSummaryRepository.AddRange(summaries);
        }
    }
}
