using Entities = Campaign.Shared.DataBaseContext.Entities;

namespace Campaign.API.Services.Password
{
    public interface IPasswordService
    {
        string GeneratePassword(Entities.Users.User user, string password);
        bool VerifyPassword(Entities.Users.User user, string hashedPassword, string password);
    }
}
