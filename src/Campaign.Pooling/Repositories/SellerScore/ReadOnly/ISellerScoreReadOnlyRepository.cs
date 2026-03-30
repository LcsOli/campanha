using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Repositories.SellerScore.ReadOnly
{
    public interface ISellerScoreReadOnlyRepository
    {
        Task<List<int>> GetSellersInserteds(int[] sellersIds);
        Task<List<Entity.SellerScore>> GetAll();
    }
}
