using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Repositories.SellerManager.ReadOnly
{
    public interface ISellerManagerScoreReadOnlyRepository
    {
        Task<List<Entity.SellerManagerScore>> GetAll();
    }
}
