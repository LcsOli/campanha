using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.API.Repositories.ProductPromotion.WriteOnly
{
    public class SellerScoreWriteOnlyRepository : ISellerScoreWriteOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public SellerScoreWriteOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public void Update(Entity.SellerScore entity)
        {
            _context.SellerScores.Update(entity);
        }

        public async Task Add(Entity.SellerScore entity)
        {
            await _context.SellerScores.AddAsync(entity);
        }
    }
}
