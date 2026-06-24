using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.Processor.API.Repositories.SellerScoreClientSummary.WriteOnly
{
    public class SellerScoreClientSummaryRepository : ISellerScoreClientSummaryRepository
    {
        private readonly CampaingContextDb _context;
        public SellerScoreClientSummaryRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task AddRange(List<Entity.SellerScoreClientsSummary> entities)
        {
            await _context.SellerScoreClientsSummaries.AddRangeAsync(entities);
        }

        public async Task Add(Entity.SellerScoreClientsSummary entity)
        {
            await _context.SellerScoreClientsSummaries.AddAsync(entity);
        }
    }
}
