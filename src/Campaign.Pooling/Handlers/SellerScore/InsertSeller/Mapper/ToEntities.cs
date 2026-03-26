using Campaign.Shared.Mappers;
using Campaign.Pooling.Commands.Seller.Create;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Handlers.SellerScore.InsertSeller.Mapper
{
    public class ToEntities : Mapper<List<Entity.SellerScore>, List<SellersScoreToCreateCommand>>
    {
        public override List<Entity.SellerScore> Parse(MapperParam<List<SellersScoreToCreateCommand>> param)
        {
            var mapper = new ToEntity();
            return [.. param.Model.Select(seller => mapper.Parse(new MapperParamImplement<SellersScoreToCreateCommand>(seller)))];
        }
    }
}
