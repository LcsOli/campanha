using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.API.Repositories.Product.ReadOnly
{
    public class ProductReadOnlyRepository : IProductReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public ProductReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<List<Entity.Product>> GetByIds(int[] ids)
        {
            return await _context.Products.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}
