using System.Net;
using Campaign.Shared.Exceptions;
using Entity = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Handlers.ProductPromotionReadDataHistory.GetProductPromotionReadDataHistory.Validator
{
    public static class ProductPromotionWasReadValidator
    {
        public static void Validate(Entity.ProductPromotionReadDataHistory entity)
        {
            if (entity != null)
                throw new CompaignException(HttpStatusCode.Forbidden, $"A promoção {entity.PromotionCode} foi processada em: {entity.ReadAt:dd/MM/yyyy}");
        }
    }
}
