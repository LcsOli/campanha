using Campaign.API.Configuration.DataBaseContext;
using Entities = Campaign.API.Configuration.DataBaseContext.Entities;

namespace Campaign.API.Repositories.Team.WriteOnly
{
    public class TeamWriteOnlyRepository : ITeamWriteOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public TeamWriteOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task Add(Entities.Team entity)
        {
            await _context.Teams.AddAsync(entity);
        }
    }
}
