using Campaign.Pooling.Commands.ProductPromotions.Get;

namespace Campaign.Pooling.Handlers.Promotion.ProductPromotionExists
{
    public interface IProductPromotionExistsHandler
    {
        Task Handle(ProductPromotionExistsCommand cmd);
    }
}
