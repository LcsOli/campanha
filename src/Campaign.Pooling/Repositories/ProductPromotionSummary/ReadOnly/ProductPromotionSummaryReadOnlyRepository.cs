using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.Pooling.DTO.Response.ProductPromotionSummary;

namespace Campaign.Pooling.Repositories.ProductPromotionSummary.ReadOnly
{
    public class ProductPromotionSummaryReadOnlyRepository : IProductPromotionSummaryReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public ProductPromotionSummaryReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<ProductPromotionsSummariesDatesInitAndEnd> GetOldAndNewProductPromotionsCodeDates(int oldPromotionCode, int currentPromotionCode)
        {
            var productPromotionsSummaries = await _context.ProductPromotionSummaries.Where(p => p.Id == oldPromotionCode || p.Id == currentPromotionCode)
                                                                                     .ToListAsync();

            var oldProductPromotionSummary = productPromotionsSummaries.FirstOrDefault(p => p.Id == oldPromotionCode);
            var newProductPromotionSummary = productPromotionsSummaries.FirstOrDefault(p => p.Id == currentPromotionCode);


            return new ProductPromotionsSummariesDatesInitAndEnd(oldProductPromotionSummary!.InitIn,
                                                                 oldProductPromotionSummary.EndIn,
                                                                 newProductPromotionSummary!.InitIn,
                                                                 newProductPromotionSummary.EndIn);
        }
    }
}
