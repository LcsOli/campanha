using Campaign.Shared.DTOs.Response.Product;
using Campaign.Pooling.Commands.ProductPromotions.Get;

namespace Campaign.Pooling.Handlers.Promotion.GetProductsPromotions
{
    public interface IGetProductsPromotionsHandler
    {
        Task<List<ProductPromotionResponse>> Handle(GetProductsPromotionsCommand cmd);
    }
}
