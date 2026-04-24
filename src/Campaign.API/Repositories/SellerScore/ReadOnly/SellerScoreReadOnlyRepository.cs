using Microsoft.EntityFrameworkCore;
using Campaign.API.DTO.SellerScore.Response;
using Campaign.Shared.DataBaseContext.Entities;
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

        //TODO - Refator select. Encontrar uma forma melhor de ranquear os vendedores.
        public async Task<List<SellerScoreResponse>> GetByFilters(int? teamId, string? filter, int? page, int? size)
        {
            filter = filter?.ToLower() ?? null;

            var query = _context.SellerScores.Include(s => s.Team)
                                             .Where(s => (teamId == null || s.TeamId == teamId) &&
                                                        (
                                                          filter == null ||
                                                          (
                                                              s.Name.ToLower().Contains(filter) ||
                                                              s.SellerId.ToString() == filter
                                                          )
                                                        )
                                             )
                                             .OrderByDescending(s => s.Score)
                                             .Select(s => new Entity.SellerScore(name: s.Name,
                                                                                 sellerId: s.SellerId,
                                                                                 score: s.Score,
                                                                                 team: s.Team!,
                                                                                 managerName: s.ManagerName,
                                                                                 revenueTarget: s.RevenueTarget,
                                                                                 currentRevenue: s.CurrentRevenue,
                                                                                 revenueMonth1: s.RevenueMonth1,
                                                                                 revenueMonth2: s.RevenueMonth2,
                                                                                 revenueMonth3: s.RevenueMonth3,
                                                                                 revenueMonth4: s.RevenueMonth4,
                                                                                 revenueMonth5: s.RevenueMonth5));

            if (filter == null && (page != null && size != null))
            {
                query = query.Skip((page.Value - 1) * size.Value)
                             .Take(size.Value);
            }

            var sellersScores = await query.ToListAsync();

            var allSellersScoresQuery = _context.SellerScores
                                             .Select(s => new
                                             {
                                                 s.TeamId,
                                                 s.SellerId,
                                                 s.Score
                                             });

            if (teamId != null)
            {
                allSellersScoresQuery = allSellersScoresQuery.Where(s => s.TeamId == teamId.Value);
            }

            var allSellersScores = await allSellersScoresQuery.OrderByDescending(s => s.Score).ToListAsync();

            var ranking = allSellersScores.Select((s, i) => new
            {
                Index = i,
                s.SellerId,
                s.Score
            });

            return [.. sellersScores.Select(s =>
            {
                var revenue = s.CurrentRevenue > 0 ? s.CurrentRevenue :
                   s.RevenueMonth5 > 0 ? s.RevenueMonth5 :
                   s.RevenueMonth4 > 0 ? s.RevenueMonth4 :
                   s.RevenueMonth3 > 0 ? s.RevenueMonth3 :
                   s.RevenueMonth2 > 0 ? s.RevenueMonth2 :
                   s.RevenueMonth1 > 0 ? s.RevenueMonth1 :
                   0;

                return new SellerScoreResponse(
                    Score: s.Score,
                    SellerName: s.Name,
                    SellerId: s.SellerId,
                    TeamName: s.Team!.Name,
                    CurrentRevenue: revenue,
                    RevenueTarget: s.RevenueTarget,
                    SellerManagerName: s.ManagerName,
                    Ranking: string.Concat(ranking.FirstOrDefault(r => r.SellerId == s.SellerId)?.Index + 1, '°'),
                    RevenueTargetPercentage: revenue > 0 ? string.Concat((revenue * 100) / s.RevenueTarget, '%') : "0%");
            })];
        }

        public async Task<decimal> GetByTeamIdCount(int? teamId, string? filter, int? page, int? size)
        {
            var query = _context.SellerScores.Include(s => s.Team)
                                                         .Where(s => (teamId == null || s.TeamId == teamId) &&
                                                                    (
                                                                      filter == null ||
                                                                      (
                                                                          s.Name.ToLower().Contains(filter) ||
                                                                          s.SellerId.ToString() == filter
                                                                      )
                                                                    )
                                                         )
                                                         .OrderByDescending(s => s.Score)
                                                         .Select(s => s.Id);

            if (page != null && size != null)
            {
                query = query.Skip((page.Value - 1) * size.Value)
                             .Take(size.Value);
            }

            return Math.Ceiling((decimal)await query.CountAsync());
        }
    }
}
