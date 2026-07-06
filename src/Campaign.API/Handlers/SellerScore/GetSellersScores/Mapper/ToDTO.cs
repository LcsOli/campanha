using Campaign.Shared.Mappers;
using Campaign.API.DTO.SellerScore.Response;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.API.Handlers.SellerScore.GetSellersScores.Mapper
{
    public class ToDTO : Mapper<SellerScoreResponse, Entity.SellerScore>
    {
        public override SellerScoreResponse Parse(MapperParam<Entity.SellerScore> param)
        {
            var model = param.Model;
            var revenue = model.CurrentRevenue > 0 ? model.CurrentRevenue :
                          model.RevenueMonth5 > 0 ? model.RevenueMonth5 :
                          model.RevenueMonth4 > 0 ? model.RevenueMonth4 :
                          model.RevenueMonth3 > 0 ? model.RevenueMonth3 :
                          model.RevenueMonth2 > 0 ? model.RevenueMonth2 :
                          model.RevenueMonth1 > 0 ? model.RevenueMonth1 : 0;

            return new(Ranking: null!,
                       Score: model.Score,
                       Coupons: model.Coupons,
                       SellerName: model.Name,
                       CurrentRevenue: revenue,
                       SellerId: model.SellerId,
                       TeamName: model.Team!.Name,
                       RevenueTarget: model.RevenueTarget,
                       SellerManagerName: model.ManagerName,
                       LastScoreByAccess: model.LastScoreByAccess,
                       QtyConsumersRegistereds: model.QtyConsumersRegistereds,
                       QtyConsumersReactivateds: model.QtyConsumersReactivateds,
                       RevenueTargetPercentage: revenue > 0 && model.RevenueTarget > 0 ? $"{(revenue * 100) / model.RevenueTarget}%" : "0%");
        }
    }
}
