using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Handlers.SellerManager.GetSellers
{
    public interface IGetSellersManagersHandler
    {
        Task<List<Entity.SellerManager>> Handle();
    }
}
