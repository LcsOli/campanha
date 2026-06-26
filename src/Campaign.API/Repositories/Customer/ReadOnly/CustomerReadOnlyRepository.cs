using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Customer;

namespace Campaign.API.Repositories.Customer.ReadOnly
{
    public class CustomerReadOnlyRepository : ICustomerReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public CustomerReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<Entity.Customer>> GetByIds(int[] id)
        {
            return await _context.Customers.Where(c => id.Contains(c.Id)).ToListAsync();
        }
    }
}
