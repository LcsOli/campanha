using Campaign.Pooling.Commands.ProductPromotions.Get;
using Entity = Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.Pooling.Handlers.ProductPromotion.GetProductsPromotions
{
    public interface IGetProductsPromotionsHandler
    {
        Task<List<Entity.ProductPromotion>> Handle(GetProductsPromotionsCommand cmd);
    }
}
