using Campaign.Pooling.DTO.Response.ProductPromotionSummary;

namespace Campaign.Pooling.Repositories.ProductPromotionSummary.ReadOnly
{
    public interface IProductPromotionSummaryReadOnlyRepository
    {
        Task<ProductPromotionsSummariesDatesInitAndEnd> GetOldAndNewProductPromotionsCodeDates(int oldPromotionCode, int currentPromotionCode);
    }
}
