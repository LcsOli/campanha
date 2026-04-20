using Campaign.Shared.Mappers;
using Entity = Campaign.Shared.DataBaseContext.Entities.Product;
using Campaign.Pooling.Commands.ProductPromotionReadHistory.Create;

namespace Campaign.Pooling.Handlers.ProductPromotionReadDataHistory.RegisterNewHistory.Mapper
{
    public class ToEntity : Mapper<Entity.ProductPromotionReadDataHistory, RegisterProductPromotionReadDataHistoryCommand>
    {
        public override Entity.ProductPromotionReadDataHistory Parse(MapperParam<RegisterProductPromotionReadDataHistoryCommand> param)
        {
            return new(param.Model.PromotionCode, DateTime.Now);
        }
    }
}
