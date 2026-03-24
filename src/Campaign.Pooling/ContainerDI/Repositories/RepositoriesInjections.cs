using Campaign.Pooling.Repositories.ProductpromotionReadDataHistory.ReadOnly;

namespace Campaign.Pooling.ContainerDI.Repositories
{
    public static class RepositoriesInjections
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductPromotionReadDataHistoryRepositorie, ProductPromotionReadDataHistoryRepositorie>();
        }
    }
}
