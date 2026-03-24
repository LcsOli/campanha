using Microsoft.AspNetCore.Identity;
using Entities = Campaign.Shared.DataBaseContext.Entities;

namespace Campaign.API.Services.Password
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHasher<Entities.Users.User> _passwordHasher;
        public PasswordService(IPasswordHasher<Entities.Users.User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string GeneratePassword(Entities.Users.User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyPassword(Entities.Users.User user, string hashedPassword, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
