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
    }
}
