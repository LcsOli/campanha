using Campaign.API.DTO.SellerManager.Response;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.API.Repositories.SellerManager.ReadOnly
{
    public interface ISellerManagerReadOnlyRepository
    {
        Task<bool> Exists(int sellerScoreId);
        Task<List<Entity.SellerManagerScore>> GetAll();
        Task<SellerManagerRevenueInfosResponse> GetRevenueTarget(int? id);
    }
}
