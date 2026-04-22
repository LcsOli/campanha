using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Team;

namespace Campaign.API.Repositories.Team.ReadOnly
{
    public class TeamReadOnlyRepository : ITeamReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public TeamReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<Entity.Team>> GetAll()
        {
            return await _context.Teams.ToListAsync();
        }
    }
}
