using Campaign.Shared.DataBaseContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace Campaign.Pooling.Repositories.ProductpromotionReadDataHistory.ReadOnly
{
    public class ProductPromotionReadDataHistoryRepositorie : IProductPromotionReadDataHistoryRepositorie
    {
        private readonly CampaingContextDb _context;
        public ProductPromotionReadDataHistoryRepositorie(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<DateTime?> GetDateOfMostRecent()
        {
            //TODO - Verificar como a busca irá se comportar caso não seja encontrado nada.
            return await _context.PromotionReadDataHistories.MaxAsync(p => p.ReadAt);
        }
    }
}
