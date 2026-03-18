using Entities = Campaign.API.Configuration.DataBaseContext.Entities;

namespace Campaign.API.Repositories.User.WriteOnly
{
    public interface IUserWriteOnlyRepository
    {
        Task Add(Entities.User entity);
    }
}
