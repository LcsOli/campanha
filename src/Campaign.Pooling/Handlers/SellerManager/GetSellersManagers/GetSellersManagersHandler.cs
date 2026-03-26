using Campaign.Pooling.Repositories.SellerManager.ReadOnly;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Handlers.SellerManager.GetSellers
{
    public class GetSellersManagersHandler : IGetSellersManagersHandler
    {
        private readonly ISellerMangerReadOnlyRepository _sellerMangerReadOnlyRepository;
        public GetSellersManagersHandler(ISellerMangerReadOnlyRepository sellerMangerReadOnlyRepository)
        {
            _sellerMangerReadOnlyRepository = sellerMangerReadOnlyRepository;
        }

        public async Task<List<Entity.SellerManager>> Handle()
        {
            return await _sellerMangerReadOnlyRepository.GetAll();
        }
    }
}
