using Entity = Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.Processor.API.Repositories.SellerScoreProductsSummary.WriteOnly
{
    public interface ISellerScoreProductSummaryWriteOnlyRepository
    {
        Task AddRange(List<Entity.SellerScoreProductsSummary> entities);
        Task Add(Entity.SellerScoreProductsSummary entity);
        void RemoveRange(List<Entity.SellerScoreProductsSummary> entities);
    }
}
