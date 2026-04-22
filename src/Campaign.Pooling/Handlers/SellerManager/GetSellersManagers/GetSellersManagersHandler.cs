using Campaign.Pooling.Repositories.SellerManager.ReadOnly;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Handlers.SellerManager.GetSellers
{
    public class GetSellersManagersHandler : IGetSellersManagersHandler
    {
        private readonly ISellerMangerScoreReadOnlyRepository _sellerMangerReadOnlyRepository;
        public GetSellersManagersHandler(ISellerMangerScoreReadOnlyRepository sellerMangerReadOnlyRepository)
        {
            _sellerMangerReadOnlyRepository = sellerMangerReadOnlyRepository;
        }

        public async Task<List<Entity.SellerManagerScore>> Handle()
        {
            return await _sellerMangerReadOnlyRepository.GetAll();
        }
    }
}
