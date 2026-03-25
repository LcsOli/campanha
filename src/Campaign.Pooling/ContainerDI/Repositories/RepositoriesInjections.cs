using Campaign.Pooling.Repositories.Products.ProductPromotionReadDataHistory.ReadOnly;

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
