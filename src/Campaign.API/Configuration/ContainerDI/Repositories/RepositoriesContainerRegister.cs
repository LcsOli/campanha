using Campaign.API.Repositories.User.ReadOnly;
using Campaign.API.Repositories.User.WriteOnly;
using Campaign.API.Handlers.User.UpdateLastAccess;
using Campaign.API.Repositories.SellerManager.ReadOnly;
using Campaign.API.Repositories.ProductPromotion.ReadOnly;
using Campaign.API.Repositories.ProductPromotion.WriteOnly;
using Campaign.Pooling.Repositories.ProductPromotionSummary.ReadOnly;

namespace Campaign.API.Configuration.ContainerDI.Repositories
{
    public static class RepositoriesContainerRegister
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserWriteOnlyRepository>();
            services.AddScoped<IUserUpdateLastAccessHandler, UserUpdateLastAccessHandler>();
            services.AddScoped<ISellerScoreReadOnlyRepository, SellerScoreReadOnlyRepository>();
            services.AddScoped<ISellerScoreWriteOnlyRepository, SellerScoreWriteOnlyRepository>();
            services.AddScoped<ISellerManagerReadOnlyRepository, SellerManagerReadOnlyRepository>();
            services.AddScoped<IProductPromotionSummaryReadOnlyRepository, ProductPromotionSummaryReadOnlyRepository>();
        }
    }
}
