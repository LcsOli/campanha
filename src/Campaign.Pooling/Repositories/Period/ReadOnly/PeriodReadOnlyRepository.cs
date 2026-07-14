using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Period;

namespace Campaign.Pooling.Repositories.Period.ReadOnly
{
    public class PeriodReadOnlyRepository : IPeriodReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public PeriodReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<Entity.Period>> GetByYear(int year)
        {
            return await _context.Periods.Where(p => p.Year == year).AsNoTracking().ToListAsync();
        }

        public async Task<int[]> GetPromotionsCodesByPeriod(int promotionCode)
        {
            var query = from period in
                        (
                            from p in _context.Periods
                            from pcs in _context.ProductPromotionSummaries
                            where
                                p.Year == pcs.EndIn.Year &&
                                pcs.Id == promotionCode &&
                                (
                                    pcs.InitIn >= p.InitIn && pcs.EndIn <= p.EndIn
                                ) &&
                                pcs.EndIn == p.EndIn
                            select p
                        )
                        from promotions in _context.ProductPromotionSummaries
                        where
                            promotions.InitIn >= period.InitIn && 
                            promotions.EndIn <= period.EndIn
                        select 
                            promotions.Id;

            return await query.ToArrayAsync();
        }
    }
}
