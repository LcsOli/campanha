using Campaign.Shared.Mappers;
using Campaign.API.DTO.SellerScore.Response;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.API.Handlers.SellerScore.GetSellersScores.Mapper
{
    public class ToDTO : Mapper<List<SellerScoreResponse>, List<Entity.SellerScore>>
    {
        public override List<SellerScoreResponse> Parse(MapperParam<List<Entity.SellerScore>> param)
        {
            var model = param.Model;
            var sellersScores = model.OrderByDescending(s => s.Score)
                                     .Select((s, index) =>
                                     {
                                         var revenue = DefineRevenue(s);
                                         return new SellerScoreResponse(
                                             Score: s.Score,
                                             SellerName: s.Name,
                                             SellerId: s.SellerId,
                                             TeamName: s.Team!.Name,
                                             CurrentRevenue: revenue,
                                             RevenueTarget: s.RevenueTarget,
                                             SellerManagerName: s.ManagerName,
                                             Ranking: string.Concat((index + 1), 'º'),
                                             RevenueTargetPercentage: revenue > 0 ? string.Concat((revenue * 100) / s.RevenueTarget, '%') : "0%");
                                        }
                                     );

            return [.. sellersScores];
        }

        private decimal DefineRevenue(Entity.SellerScore sellerScore)
        {
            return sellerScore.CurrentRevenue > 0 ? sellerScore.CurrentRevenue :
                   sellerScore.RevenueMonth5 > 0 ? sellerScore.RevenueMonth5 :
                   sellerScore.RevenueMonth4 > 0 ? sellerScore.RevenueMonth4 :
                   sellerScore.RevenueMonth3 > 0 ? sellerScore.RevenueMonth3 :
                   sellerScore.RevenueMonth2 > 0 ? sellerScore.RevenueMonth2 :
                   sellerScore.RevenueMonth1 > 0 ? sellerScore.RevenueMonth1 :
                   0;
        }
    }
}
