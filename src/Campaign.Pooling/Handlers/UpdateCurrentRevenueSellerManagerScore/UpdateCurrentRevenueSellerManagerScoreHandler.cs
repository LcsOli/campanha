using Campaign.Pooling.Commands.SellerManager.Update;
using Campaign.Pooling.Repositories.SellerManager.ReadOnly;
using Campaign.Pooling.Repositories.SellerManager.WriteOnly;

namespace Campaign.Pooling.Handlers.UpdateCurrentRevenueSellerManager
{
    public class UpdateCurrentRevenueSellerManagerScoreHandler : IUpdateCurrentRevenueSellerManagerScoreHandler
    {
        private readonly ISellerManagerScoreReadOnlyRepository _sellerMangerReadOnlyRepository;
        private readonly ISellerManagerScoreWriteOnlyRepository _sellerManagerWriteOnlyRepository;
        public UpdateCurrentRevenueSellerManagerScoreHandler(ISellerManagerScoreReadOnlyRepository sellerMangerReadOnlyRepository,
                                                             ISellerManagerScoreWriteOnlyRepository sellerManagerWriteOnlyRepository)
        {
            _sellerMangerReadOnlyRepository = sellerMangerReadOnlyRepository;
            _sellerManagerWriteOnlyRepository = sellerManagerWriteOnlyRepository;
        }

        public async Task Handle(UpdateCurrentRevenueSellerManagerCommand cmd)
        {
            var sellersManagers = await _sellerMangerReadOnlyRepository.GetAll();

            sellersManagers.ForEach(sellerManager =>
            {
                var revenue = cmd.SellersScores.Where(s => s.SellerManagerId == sellerManager.Code)
                                 .Sum(s => s.RevenueMonth1 + s.RevenueMonth2 + s.RevenueMonth3 + s.RevenueMonth4 + s.RevenueMonth5);

                sellerManager.UpdateCurrentRevenue(revenue);
            });

            _sellerManagerWriteOnlyRepository.UpdateAll(sellersManagers);
        }
    }
}
