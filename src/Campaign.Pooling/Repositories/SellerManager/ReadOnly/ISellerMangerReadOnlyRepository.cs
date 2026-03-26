using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Repositories.SellerManager.ReadOnly
{
    public interface ISellerMangerReadOnlyRepository
    {
        Task<List<Entity.SellerManager>> GetAll();
    }
}
