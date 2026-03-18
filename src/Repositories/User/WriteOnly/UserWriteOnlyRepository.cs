using Campaign.API.Configuration.DataBaseContext;
using Entities = Campaign.API.Configuration.DataBaseContext.Entities;

namespace Campaign.API.Repositories.User.WriteOnly
{
    public class UserWriteOnlyRepository : IUserWriteOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public UserWriteOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task Add(Entities.User entity)
        {
            await _context.Users.AddAsync(entity);
        }
    }
}
