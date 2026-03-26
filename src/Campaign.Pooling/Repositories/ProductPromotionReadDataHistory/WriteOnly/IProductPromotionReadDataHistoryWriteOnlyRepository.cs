using Entity = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.WriteOnly
{
    public interface IProductPromotionReadDataHistoryWriteOnlyRepository
    {
        Task AddAsync(Entity.ProductPromotionReadDataHistory entity);
    }
}
