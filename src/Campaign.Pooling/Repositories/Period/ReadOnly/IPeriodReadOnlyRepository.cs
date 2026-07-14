using Entity = Campaign.Shared.DataBaseContext.Entities.Period;

namespace Campaign.Pooling.Repositories.Period.ReadOnly
{
    public interface IPeriodReadOnlyRepository
    {
        Task<List<Entity.Period>> GetByYear(int year);
        Task<int[]> GetPromotionsCodesByPeriod(int promotionCode);
    }
}
