using Campaign.API.DTO.SellerManager.Response;
using Campaign.Shared.Mappers;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.API.Handlers.SellerManagerScore.GetRevenue.Mapper
{
    public class ToDTO : Mapper<List<SellerManagerScoreResponse>, List<Entity.SellerManagerScore>>
    {
        public override List<SellerManagerScoreResponse> Parse(MapperParam<List<Entity.SellerManagerScore>> param)
        {
            var sellersManagersScore = param.Model.OrderByDescending(s => s.CurrentRevenue)
                                                  .ThenBy(s => s.Name)
                                                  .GroupBy(s => s.Name)
                                                  .Select((s, i) =>
                                                  {
                                                      return new SellerManagerScoreResponse(
                                                          Name: s.Key,
                                                          Ranking: string.Concat((i + 1), 'º'),
                                                          CurrentRevenue: s.Select(r => r.CurrentRevenue).Sum(),
                                                          TargetRevenue: s.Select(r => r.TargetRevenue).First()
                                                  
                                                      );
                                                  });

            return [.. sellersManagersScore];
        }
    }
}
