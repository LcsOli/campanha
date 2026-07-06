using Campaign.Pooling.Repositories.OrderDetail.ReadOnly;
using Campaign.Pooling.Repositories.OrderProductRemoved.ReadOnly;
using Campaign.Pooling.Repositories.OrderSummary.ReadOnly;
using Campaign.Pooling.Repositories.Period.ReadOnly;
using Campaign.Pooling.Repositories.ProductPromotion.ReadOnly;
using Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.ReadOnly;
using Campaign.Pooling.Repositories.ProductPromotionReadDataHistory.WriteOnly;
using Campaign.Pooling.Repositories.ProductPromotionSummary.ReadOnly;
using Campaign.Pooling.Repositories.SellerManager.ReadOnly;
using Campaign.Pooling.Repositories.SellerManager.WriteOnly;
using Campaign.Pooling.Repositories.Sellers.Seller.ReadOnly;
using Campaign.Pooling.Repositories.SellerScore.ReadOnly;
using Campaign.Pooling.Repositories.SellerScore.WriteOnly;
using Campaign.Processor.API.Repositories.SellerScoreClientSummary.WriteOnly;
using Campaign.Processor.API.Repositories.SellerScoreProductsSummary.WriteOnly;

namespace Campaign.Pooling.Configurations.ContainerDI.Repositories
{
    public static class RepositoryContainerRegister
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPeriodReadOnlyRepository, PeriodReadOnlyRepository>();
            services.AddScoped<ISellerReadOnlyRepository, SellerReadOnlyRepository>();
            services.AddScoped<ISellerScoreReadOnlyRepository, SellerScoreReadOnlyRepository>();
            services.AddScoped<IOrderDetailReadOnlyRepository, OrderDetailReadOnlyRepository>();
            services.AddScoped<IOrderSummaryReadOnlyRepository, OrderSummaryReadOnlyRepository>();
            services.AddScoped<ISellerScoreWriteOnlyRepository, SellerScoreWriteOnlyRepository>();
            services.AddScoped<IProductPromotionReadOnlyRepository, ProductPromotionReadOnlyRepository>();
            services.AddScoped<ISellerManagerScoreReadOnlyRepository, SellerManagerScoreReadOnlyRepository>();
            services.AddScoped<IOrderProductRemovedReadOnlyRepository, OrderProductRemovedReadOnlyRepository>();
            services.AddScoped<ISellerManagerScoreWriteOnlyRepository, SellerManagerScoreWriteOnlyRepository>();
            services.AddScoped<IProductPromotionSummaryReadOnlyRepository, ProductPromotionSummaryReadOnlyRepository>();
            services.AddScoped<IProductPromotionReadDataHistoryRepositorie, ProductPromotionReadDataHistoryRepositorie>();
            services.AddScoped<ISellerScoreClientSummaryWriteOnlyRepository, SellerScoreClientSummaryWriteOnlyRepository>();
            services.AddScoped<ISellerScoreProductSummaryWriteOnlyRepository, SellerScoreProductSummaryWriteOnlyRepository>();
            services.AddScoped<IProductPromotionReadDataHistoryWriteOnlyRepository, ProductPromotionReadDataHistoryWriteOnlyRepository>();
        }
    }
}
