using EntityOrder = Campaign.Shared.DataBaseContext.Entities.Order;
using EntityProduct = Campaign.Shared.DataBaseContext.Entities.Product;
using EntitySellerScore = Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Pooling.Commands.CalculateScoreByProduct
{
    public record CalculateScoreByProductCommand(List<EntityOrder.OrderDetail> OrdersDetails,
                                                 List<EntitySellerScore.SellerScore> SellersScores,
                                                 List<EntityProduct.ProductPromotionSummary> ProductsPromotions);
}
