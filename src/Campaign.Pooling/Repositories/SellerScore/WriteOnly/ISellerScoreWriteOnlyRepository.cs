using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Repositories.SellerScore.WriteOnly
{
    public interface ISellerScoreWriteOnlyRepository
    {
        Task AddAsync(List<Entity.SellerScore> entities);
        void Update(List<Entity.SellerScore> entities);
    }
}
