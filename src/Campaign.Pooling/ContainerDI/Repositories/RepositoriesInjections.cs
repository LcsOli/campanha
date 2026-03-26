using Campaign.Pooling.Repositories.SellerScore.WriteOnly;
using Campaign.Pooling.Repositories.SellerManager.ReadOnly;
using Campaign.Pooling.Repositories.Products.ProductPromotionReadDataHistory.ReadOnly;
using Campaign.Pooling.Handlers.SellerManager.GetSellers;

namespace Campaign.Pooling.ContainerDI.Repositories
{
    public static class RepositoriesInjections
    {
        //TODO - Verificar os repositórios que ainda não foram inseridos no container
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISellerMangerReadOnlyRepository, SellerMangerReadOnlyRepository>();
            services.AddScoped<ISellerScoreWriteOnlyRepository, SellerScoreWriteOnlyRepository>();
            services.AddScoped<IProductPromotionReadDataHistoryRepositorie, ProductPromotionReadDataHistoryRepositorie>();
        }
    }
}
