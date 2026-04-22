using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.API.Repositories.ProductPromotion.ReadOnly
{
    public interface ISellerScoreReadOnlyRepository
    {
        Task<Entity.SellerScore?> GetBySellerId(int sellerId);
        Task<List<Entity.SellerScore>> GetAll();
        Task<List<Entity.SellerScore>> GetByTeamId(int teamId, string? filter, int page, int size);
        Task<decimal> GetByTeamIdCount(int teamId, string? filter, int page, int size);
    }
}
