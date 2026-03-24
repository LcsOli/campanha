using Entities = Campaign.Shared.DataBaseContext.Entities;

namespace Campaign.API.Repositories.Team.WriteOnly
{
    public interface ITeamWriteOnlyRepository
    {
        Task Add(Entities.Team entity);
    }
}
