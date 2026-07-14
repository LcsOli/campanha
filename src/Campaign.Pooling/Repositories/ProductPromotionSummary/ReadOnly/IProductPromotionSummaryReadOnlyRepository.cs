using Campaign.Pooling.DTO.Response.ProductPromotionSummary;
using Entity = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Repositories.ProductPromotionSummary.ReadOnly
{
    public interface IProductPromotionSummaryReadOnlyRepository
    {
        Task<ProductPromotionSummariesDatesResponse?> GetProductPromotionSummariesDates(int currentPromotionCode);
        Task<Entity.ProductPromotionSummary?> GetByPromotionCode(int promotionCode);
        Task<int[]> GetPromotionsCodesByPeriod(DateTime initIn, DateTime endIn);
    }
}
