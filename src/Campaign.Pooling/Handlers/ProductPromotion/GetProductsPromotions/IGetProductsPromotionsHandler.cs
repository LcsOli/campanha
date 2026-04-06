using Campaign.Pooling.Commands.ProductPromotions.Get;
using Campaign.Shared.DTOs.Response.Product;
using Entity = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Handlers.ProductPromotion.GetProductsPromotions
{
    public interface IGetProductsPromotionsHandler
    {
        Task<List<ProductPromotionResponse>> Handle(GetProductsPromotionsCommand cmd);
    }
}
