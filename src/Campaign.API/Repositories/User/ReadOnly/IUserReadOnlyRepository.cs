using Entities = Campaign.API.Configuration.DataBaseContext.Entities;

namespace Campaign.API.Repositories.User.ReadOnly
{
    public interface IUserReadOnlyRepository
    {
        Task<Entities.Users.User?> GetById(int id);
        Task<Entities.Users.User?> GetByDocument(string document);
    }
}
