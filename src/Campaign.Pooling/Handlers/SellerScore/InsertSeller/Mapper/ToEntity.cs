using Campaign.Shared.Mappers;
using Campaign.Pooling.Commands.Seller.Create;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Handlers.SellerScore.InsertSeller.Mapper
{
    public class ToEntity : Mapper<Entity.SellerScore, SellersScoreToCreateCommand>
    {
        public override Entity.SellerScore Parse(MapperParam<SellersScoreToCreateCommand> param)
        {
            return new(param.Model.TeamId,
                       param.Model.Name,
                       param.Model.SellerId,
                       param.Model.ManagerName,
                       param.Model.SellerManagerId);
        }
    }
}