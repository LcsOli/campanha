using Campaign.API.Service.GenerateToken;

namespace Campaign.API.Configuration.Container_DI
{
    public static class ServicesInject
    {
        public static void AddServicesInject(this IServiceCollection services)
        {
            services.AddScoped<IJwtToken, JwtToken>();
        }
    }
}
