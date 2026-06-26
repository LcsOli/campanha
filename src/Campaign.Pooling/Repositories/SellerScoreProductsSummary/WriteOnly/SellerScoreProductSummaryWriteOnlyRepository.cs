using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.Processor.API.Repositories.SellerScoreProductsSummary.WriteOnly
{
    public class SellerScoreProductSummaryWriteOnlyRepository : ISellerScoreProductSummaryWriteOnlyRepository
    {
        private readonly CampaingContextDb _context;

        public SellerScoreProductSummaryWriteOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task AddRange(List<Entity.SellerScoreProductsSummary> entities)
        {
            await _context.SellerScoreProductsSummaries.AddRangeAsync(entities);
        }

        public async Task Add(Entity.SellerScoreProductsSummary entity)
        {
            await _context.SellerScoreProductsSummaries.AddAsync(entity);
        }
    }
}
