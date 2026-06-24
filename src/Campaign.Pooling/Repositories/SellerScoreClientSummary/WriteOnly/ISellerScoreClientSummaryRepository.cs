using Entity = Campaign.Shared.DataBaseContext.Entities.SellerScoreSummaries;

namespace Campaign.Processor.API.Repositories.SellerScoreClientSummary.WriteOnly
{
    public interface ISellerScoreClientSummaryRepository
    {
        Task AddRange(List<Entity.SellerScoreClientsSummaries> entities);
        Task Add(Entity.SellerScoreClientsSummaries entity);
    }
}
