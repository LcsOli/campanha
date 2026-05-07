using Entity = Campaign.Shared.DataBaseContext.Entities.Period;

namespace Campaign.API.Repositories.Period.ReadOnly
{
    public interface IPeriodReadOnlyRepository
    {
        Task<Entity.Period?> GetFirst();
    }
}
