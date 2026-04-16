using Campaign.API.Services.Password;
using Campaign.API.Services.GenerateToken;

namespace Campaign.API.Configuration.Container_DI
{
    public static class ServicesContainerRegister
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtToken, JwtToken>();
            services.AddScoped<IPasswordService, PasswordService>();
        }
    }
}
