using Entity = Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.Processor.API.Repositories.SellerScoreProductsSummary.WriteOnly
{
    public interface ISellerScoreProductSummaryRepository
    {
        Task AddRange(List<Entity.SellerScoreProductsSummary> entities);
        Task Add(Entity.SellerScoreProductsSummary entity);
    }
}
