using Campaign.Shared.DataBaseContext.Entities;
using Entities = Campaign.Shared.DataBaseContext.Entities;

namespace Campaign.API.Repositories.User.WriteOnly
{
    public class UserWriteOnlyRepository : IUserWriteOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public UserWriteOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task AddAsync(Entities.Users.User entity)
        {
            await _context.Users.AddAsync(entity);
        }

        public void Update(Entities.Users.User entity)
        {
            _context.Users.Update(entity);
        }
    }
}
