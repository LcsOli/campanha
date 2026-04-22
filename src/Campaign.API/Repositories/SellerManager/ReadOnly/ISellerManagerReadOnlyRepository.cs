using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.API.Repositories.SellerManager.ReadOnly
{
    public interface ISellerManagerReadOnlyRepository
    {
        Task<List<Entity.SellerManagerScore>> GetAll();
    }
}
