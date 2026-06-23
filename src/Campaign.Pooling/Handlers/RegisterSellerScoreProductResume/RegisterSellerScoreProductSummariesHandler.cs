using Campaign.Processor.API.Commands.Summaries.Create;
using Campaign.Shared.DataBaseContext.Entities.UnityOfWork;
using Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;
using Campaign.Processor.API.Repositories.SellerScoreProductsSummary.WriteOnly;

namespace Campaign.Processor.API.Handlers.RegisterSellerScoreProductResume
{
    public class RegisterSellerScoreProductSummariesHandler : IRegisterSellerScoreProductSummariesHandler
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly ISellerScoreProductSummaryRepository _sellerScoreProductSummaryRepository;

        public RegisterSellerScoreProductSummariesHandler(IUnityOfWork unityOfWork,
            ISellerScoreProductSummaryRepository sellerScoreProductSummaryRepository)
        {
            _unityOfWork = unityOfWork;
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
                                                          Name = o.First().ConsumerName,
                                                          Orders = o.DistinctBy(p => p.ProductId).ToList()
                                                      }).ToList();

                if (clients.Count <= 0)
                    return;

                clients.ForEach(client =>
                {
                    var sellerersScoresProductsSummaries = client.Orders.Select(o => new SellerScoreProductsSummary(productId: o.ProductId,
                                                                                                                    clientId: client.CustomerId,
                                                                                                                    sellerId: sellerScore.SellerId,
                                                                                                                    promotionCode: cmd.PromotionCode,
                                                                                                                    points: o.ProductPromotionPoints!.Value));

                    summaries.AddRange(sellerersScoresProductsSummaries);
                });

            });

            await _sellerScoreProductSummaryRepository.AddRange(summaries);

            await _unityOfWork.SaveAsync();
        }
    }
}
