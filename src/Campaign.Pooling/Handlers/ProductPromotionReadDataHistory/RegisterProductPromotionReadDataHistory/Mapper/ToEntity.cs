using Entity = Campaign.Shared.DataBaseContext.Entities.Product;
using Campaign.Pooling.Commands.ProductPromotionReadHistory.Create;

namespace Campaign.Pooling.Handlers.ProductPromotionReadDataHistory.RegisterNewHistory.Mapper
{
    //TODO - Verificar todos os Mappers e deixar no padrão: To + Tipo de retorno.
    public static class ToEntity
    {
        public static Entity.ProductPromotionReadDataHistory Parse(RegisterProductPromotionReadDataHistoryCommand cmd)
        {
            return new(cmd.promotionCode, DateTime.Now);
        }
    }
}
