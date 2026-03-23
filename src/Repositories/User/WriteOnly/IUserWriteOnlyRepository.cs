using Entities = Campaign.API.Configuration.DataBaseContext.Entities;

namespace Campaign.API.Repositories.User.WriteOnly
{
    public interface IUserWriteOnlyRepository
    {
        Task AddAsync(Entities.Users.User entity);
        void Update(Entities.Users.User entity);
    }
}
