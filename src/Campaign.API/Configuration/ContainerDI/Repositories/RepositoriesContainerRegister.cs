using Campaign.API.Handlers.User.UpdateLastAccess;
using Campaign.API.Repositories.Customer.ReadOnly;
using Campaign.API.Repositories.Period.ReadOnly;
using Campaign.API.Repositories.Product.ReadOnly;
using Campaign.API.Repositories.ProductPromotion.ReadOnly;
using Campaign.API.Repositories.ProductPromotion.WriteOnly;
using Campaign.API.Repositories.PromotionReadDataHistory.ReadOnly;
using Campaign.API.Repositories.SellerManager.ReadOnly;
using Campaign.API.Repositories.SellerScoreClientSummary.ReadOnly;
using Campaign.API.Repositories.SellerScoreProductsSummary.ReadOnly;
using Campaign.API.Repositories.Team.ReadOnly;
using Campaign.API.Repositories.User.ReadOnly;
using Campaign.API.Repositories.User.WriteOnly;
using Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.ReadOnly;
using Campaign.Pooling.Repositories.ProductPromotionSummary.ReadOnly;

namespace Campaign.API.Configuration.ContainerDI.Repositories
{
    public static class RepositoriesContainerRegister
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITeamReadOnlyRepository, TeamReadOnlyRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserWriteOnlyRepository>();
            services.AddScoped<IPeriodReadOnlyRepository, PeriodReadOnlyRepository>();
            services.AddScoped<IProductReadOnlyRepository, ProductReadOnlyRepository>();
            services.AddScoped<ICustomerReadOnlyRepository, CustomerReadOnlyRepository>();
            services.AddScoped<IUserUpdateLastAccessHandler, UserUpdateLastAccessHandler>();
            services.AddScoped<ISellerScoreReadOnlyRepository, SellerScoreReadOnlyRepository>();
            services.AddScoped<ISellerScoreWriteOnlyRepository, SellerScoreWriteOnlyRepository>();
            services.AddScoped<ISellerManagerReadOnlyRepository, SellerManagerReadOnlyRepository>();
            services.AddScoped<IPromotionReadDataHistoryRepositorie, PromotionReadDataHistoryRepositorie>();
            services.AddScoped<IProductPromotionSummaryReadOnlyRepository, ProductPromotionSummaryReadOnlyRepository>();
            services.AddScoped<ISellerScoreClientSummaryReadOnlyRepository, SellerScoreClientSummaryReadOnlyRepository>();
            services.AddScoped<ISellerScoreProductSummaryReadOnlyRepository, SellerScoreProductSummaryReadOnlyRepository>();
        }
    }
}
