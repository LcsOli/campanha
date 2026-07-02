using Campaign.Shared.DataBaseContext.Entities.Product;

namespace Campaign.API.Repositories.PromotionReadDataHistory.ReadOnly
{
    public interface IPromotionReadDataHistoryRepositorie
    {
        Task<List<ProductPromotionSummary>> GetAll();
    }
}
