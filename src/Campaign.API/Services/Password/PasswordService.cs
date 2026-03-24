using Microsoft.AspNetCore.Identity;
using Campaign.API.Configuration.DataBaseContext.Entities.Users;

namespace Campaign.API.Services.Password
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        public PasswordService(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string GeneratePassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyPassword(User user, string hashedPassword, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
