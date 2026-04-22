using Campaign.Shared.Mappers;
using Campaign.API.DTO.SellerScore.Get;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.API.Handlers.SellerScore.GetSellersScores.Mapper
{
    public class ToDTO : Mapper<List<SellerScoreResponse>, List<Entity.SellerScore>>
    {
        public override List<SellerScoreResponse> Parse(MapperParam<List<Entity.SellerScore>> param)
        {
            var model = param.Model;
            var sellersScores = model.Select((s, index) => new SellerScoreResponse(

                                        Score: s.Score,
                                        SellerName: s.Name,
                                        SellerId: s.SellerId,
                                        TeamName: s.Team!.Name,
                                        CurrentRevenue: s.CurrentRevenue,
                                        SellerManagerName: s.ManagerName,
                                        Ranking: string.Concat((index + 1), 'º'),
                                        RevenueTargetPercentage: s.CurrentRevenue > 0 ? string.Concat(((s.CurrentRevenue * 100) / s.RevenueTarget), '%') : "0%"

                                     )).OrderBy(s => s.Score)
                                     .ThenBy(s => s.SellerName);

            return [.. sellersScores];
        }
    }
}
