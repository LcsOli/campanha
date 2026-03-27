using System.Net;
using Campaign.Shared.Exceptions;

namespace Campaign.Pooling.Handlers.ProductPromotion.ProductPromotionExists.Validator
{
    public static class ProductPromotionExistsValidator 
    {
        public static void Validate(bool productPromotion)
        {
            if (!productPromotion)
                throw new CompaignException(HttpStatusCode.BadRequest, "O código da promoção é inválido.");
        }
    }
}
