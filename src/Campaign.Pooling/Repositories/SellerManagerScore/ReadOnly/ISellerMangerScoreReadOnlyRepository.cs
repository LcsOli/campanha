using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Repositories.SellerManager.ReadOnly
{
    public interface ISellerMangerScoreReadOnlyRepository
    {
        Task<List<Entity.SellerManagerScore>> GetAll();
    }
}
