using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Repositories.SellerManager.WriteOnly
{
    public interface ISellerManagerScoreWriteOnlyRepository
    {
        void UpdateAll(List<Entity.SellerManagerScore> entities);
    }
}
