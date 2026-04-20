using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Repositories.SellerManager.WriteOnly
{
    public interface ISellerManagerWriteOnlyRepository
    {
        void UpdateAll(List<Entity.SellerManager> entities);
    }
}
