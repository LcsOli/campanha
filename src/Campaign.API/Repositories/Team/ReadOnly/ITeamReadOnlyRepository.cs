using Entity = Campaign.Shared.DataBaseContext.Entities.Team;

namespace Campaign.API.Repositories.Team.ReadOnly
{
    public interface ITeamReadOnlyRepository
    {
        Task<List<Entity.Team>> GetAll();
    }
}
