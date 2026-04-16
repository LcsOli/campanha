using Entity = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Repositories.ProductPromotionSummary.ReadOnly
{
    public interface IProductPromotionSummaryReadOnlyRepository
    {
        Task<Entity.ProductPromotionSummary?> GetByPromotionCode(int promotionCode);
        Task<Entity.ProductPromotionSummary?> GetByPeriod(DateTime period);
    }
}
