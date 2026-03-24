using Campaign.Shared.DataBaseContext.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Campaign.API.Configuration.ContainerDI.Identity
{
    public static class IdentityInjection
    {
        public static void AddIdentityInjection(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        }
    }
}
