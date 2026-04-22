using Campaign.Shared.DataBaseContext.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.API.Repositories.ProductPromotion.ReadOnly
{
    public class SellerScoreReadOnlyRepository : ISellerScoreReadOnlyRepository
    {
        private readonly CampaingContextDb _context;
        public SellerScoreReadOnlyRepository(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task<Entity.SellerScore?> GetBySellerId(int sellerId)
        {
            return await _context.SellerScores.FirstOrDefaultAsync(p => p.SellerId == sellerId);
        }

        public async Task<List<Entity.SellerScore>> GetAll()
        {
            return await _context.SellerScores.Include(s => s.Team)
                                              .ToListAsync();
        }

        public async Task<List<Entity.SellerScore>> GetByTeamId(int teamId, string? filter, int page, int size)
        {
            return await _context.SellerScores.Include(s => s.Team)
                                              .Where(s => s.TeamId == teamId &&
                                                          (
                                                            filter == null ||
                                                            (
                                                                s.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
                                                                s.SellerId.ToString() == filter
                                                            )
                                                          )
                                                       ).Skip((page - 1) * size)
                                                        .Take(size)
                                                        .ToListAsync();
        }

        public async Task<decimal> GetByTeamIdCount(int teamId, string? filter, int page, int size)
        {
            return Math.Ceiling((decimal)await _context.SellerScores.Where(s => s.TeamId == teamId &&
                                                          (
                                                            filter == null ||
                                                            (
                                                                s.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
                                                                s.SellerId.ToString() == filter
                                                            )
                                                          )
                                                       ).Select(s => s.Id)
                                                        .Skip((page - 1) * size)
                                                        .Take(size)
                                                        .CountAsync());
        }
    }
}
