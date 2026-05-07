using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Period;

namespace Campaign.API.Repositories.Period.ReadOnly
{
    public class PeriodReadOnlyRepository : IPeriodReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public PeriodReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<Entity.Period?> GetFirst()
        {
            return await _context.Periods.FirstOrDefaultAsync();
        }
    }
}
