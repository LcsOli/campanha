using Campaign.API.Configuration.DataBaseContext.Entities.Users;

namespace Campaign.API.Services.Password
{
    public interface IPasswordService
    {
        string GeneratePassword(User user, string password);
        bool VerifyPassword(User user, string hashedPassword, string password);
    }
}
