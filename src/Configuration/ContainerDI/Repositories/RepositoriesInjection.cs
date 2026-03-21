using Campaign.API.Repositories.User.ReadOnly;
using Campaign.API.Repositories.User.WriteOnly;

namespace Campaign.API.Configuration.ContainerDI.Repositories
{
    public static class RepositoriesInjection
    {
        public static void AddRepositoriesInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserWriteOnlyRepository>();
        }
    }
}
