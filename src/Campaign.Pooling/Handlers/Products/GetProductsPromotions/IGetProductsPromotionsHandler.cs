using Campaign.Pooling.Commands.Promotions;
using Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Handlers.Products.GetPromotionsProducts
{
    public interface IGetProductsPromotionsHandler
    {
        Task<List<ProductPromotion>> Handle(GetProductsPromotionsCommand cmd);
    }
}
