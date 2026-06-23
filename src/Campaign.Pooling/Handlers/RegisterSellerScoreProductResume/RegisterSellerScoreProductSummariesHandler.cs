using Campaign.Processor.API.Commands.Summaries.Create;
using Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;
using Campaign.Processor.API.Repositories.SellerScoreProductsSummary.WriteOnly;

namespace Campaign.Processor.API.Handlers.RegisterSellerScoreProductResume
{
    public class RegisterSellerScoreProductSummariesHandler : IRegisterSellerScoreProductSummariesHandler
    {
        private readonly ISellerScoreProductSummaryRepository _sellerScoreProductSummaryRepository;

        public RegisterSellerScoreProductSummariesHandler(ISellerScoreProductSummaryRepository sellerScoreProductSummaryRepository)
        {
            _sellerScoreProductSummaryRepository = sellerScoreProductSummaryRepository;
        }

        public async Task Handle(RegisterSellerScoreProductSummariesCommand cmd)
        {
            var summaries = new List<SellerScoreProductsSummary>();

            cmd.SellersScores.ForEach(sellerScore =>
            {
                var clients = cmd.OrdersDetails.Where(o => o.SellerId == sellerScore.SellerId)
                                                      .GroupBy(o => o.ConsumerId)
                                                      .Select(o => new
                                                      {
                                                          CustomerId = o.Key,
                                                          Name = o.First().ClientName,
                                                          Orders = o.DistinctBy(p => p.ProductId).ToList()
                                                      }).ToList();

                if (clients.Count <= 0)
                    return;

                clients.ForEach(client =>
                {
                    var sellerersScoresProductsSummaries = client.Orders.Select(o => new SellerScoreProductsSummary(clientName: client.Name,
                                                                                                                    sellerId: sellerScore.SellerId,
                                                                                                                    promotionCode: cmd.PromotionCode,
                                                                                                                    productName: o.ProductDescription,
                                                                                                                    points: o.ProductPromotionPoints!.Value));

                    summaries.AddRange(sellerersScoresProductsSummaries);
                });

            });

            await _sellerScoreProductSummaryRepository.AddRange(summaries);
        }
    }
}
