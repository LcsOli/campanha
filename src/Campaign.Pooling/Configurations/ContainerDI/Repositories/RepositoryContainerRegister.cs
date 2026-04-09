using Campaign.Pooling.Repositories.OrderDetail.ReadOnly;
using Campaign.Pooling.Repositories.OrderSummary.ReadOnly;
using Campaign.Pooling.Repositories.ProductPromotion.ReadOnly;
using Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.ReadOnly;
using Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.WriteOnly;
using Campaign.Pooling.Repositories.ProductPromotionSummary.ReadOnly;
using Campaign.Pooling.Repositories.SellerManager.ReadOnly;
using Campaign.Pooling.Repositories.Sellers.Seller.ReadOnly;
using Campaign.Pooling.Repositories.SellerScore.ReadOnly;
using Campaign.Pooling.Repositories.SellerScore.WriteOnly;

namespace Campaign.Pooling.Configurations.ContainerDI.Repositories
{
    public static class RepositoryContainerRegister
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISellerReadOnlyRepository, SellerReadOnlyRepository>();
            services.AddScoped<ISellerScoreReadOnlyRepository, SellerScoreReadOnlyRepository>();
            services.AddScoped<IOrderDetailReadOnlyRepository, OrderDetailReadOnlyRepository>();
            services.AddScoped<IOrderSummaryReadOnlyRepository, OrderSummaryReadOnlyRepository>();
            services.AddScoped<ISellerMangerReadOnlyRepository, SellerMangerReadOnlyRepository>();
            services.AddScoped<ISellerScoreWriteOnlyRepository, SellerScoreWriteOnlyRepository>();
            services.AddScoped<IProductPromotionReadOnlyRepository, ProductPromotionReadOnlyRepository>();
            services.AddScoped<IProductPromotionSummaryReadOnlyRepository, ProductPromotionSummaryReadOnlyRepository>();
            services.AddScoped<IProductPromotionReadDataHistoryRepositorie, ProductPromotionReadDataHistoryRepositorie>();
            services.AddScoped<IProductPromotionReadDataHistoryWriteOnlyRepository, ProductPromotionReadDataHistoryWriteOnlyRepository>();
        }
    }
}
