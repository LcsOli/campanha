using Campaign.Pooling.Repositories.SellerScore.WriteOnly;
using Campaign.Pooling.Repositories.SellerManager.ReadOnly;
using Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.ReadOnly;

namespace Campaign.Pooling.Configurations.ContainerDI.Repositories
{
    public static class RepositoryContainerRegister
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
