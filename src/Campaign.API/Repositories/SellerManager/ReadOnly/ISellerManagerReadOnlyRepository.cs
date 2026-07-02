using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.API.Repositories.SellerManager.ReadOnly
{
    public interface ISellerManagerReadOnlyRepository
    {
        Task<bool> Exists(int sellerScoreId);
        Task<List<Entity.SellerManagerScore>> GetAll();
        Task<decimal> GetRevenueTarget(int id);
    }
}
