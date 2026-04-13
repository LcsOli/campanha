using Campaign.Pooling.DTO.Response.ProductPromotionSummary;

namespace Campaign.Pooling.Repositories.ProductPromotionSummary.ReadOnly
{
    public interface IProductPromotionSummaryReadOnlyRepository
    {
        Task<ProductPromotionSummariesDatesResponse?> GetProductPromotionSummariesDates(int currentPromotionCode);
    }
}
