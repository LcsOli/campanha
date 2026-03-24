using Microsoft.EntityFrameworkCore;
using Campaign.API.Configuration.DataBaseContext;
using Entities = Campaign.API.Configuration.DataBaseContext.Entities;

namespace Campaign.API.Repositories.User.ReadOnly
{
    public class UserReadOnlyRepository : IUserReadOnlyRepository 
    {
        private readonly CampaingContextDb _context;
        public UserReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<Entities.Users.User?> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Entities.Users.User?> GetByDocument(string document)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Document == document);
        }
    }
}
