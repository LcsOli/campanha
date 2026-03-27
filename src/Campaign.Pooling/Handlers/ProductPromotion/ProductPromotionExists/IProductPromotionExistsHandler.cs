using Campaign.Pooling.Commands.ProductPromotions.Get;

namespace Campaign.Pooling.Handlers.ProductPromotion.ProductPromotionExists
{
    public interface IProductPromotionExistsHandler
    {
        Task Handle(ProductPromotionExistsCommand cmd);
    }
}
