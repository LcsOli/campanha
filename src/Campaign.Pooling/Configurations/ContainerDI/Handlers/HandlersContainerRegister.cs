using Campaign.Pooling.Handlers.CalculateCoupons;
using Campaign.Pooling.Handlers.CalculateReactivatedsConsummers;
using Campaign.Pooling.Handlers.CalculateRegisteredsConsummers;
using Campaign.Pooling.Handlers.CalculateRevenueTarget;
using Campaign.Pooling.Handlers.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.OrderDetail.GetOrdersDetail;
using Campaign.Pooling.Handlers.ProductPromotion.GetProductsPromotions;
using Campaign.Pooling.Handlers.ProductPromotion.ProductPromotionExists;
using Campaign.Pooling.Handlers.ProductPromotionHistory.GetLastProductPromotionsHistory;
using Campaign.Pooling.Handlers.ProductPromotionReadDataHistory.RegisterNewHistory;
using Campaign.Pooling.Handlers.Seller.GetSellers;
using Campaign.Pooling.Handlers.Seller.GetSellersToCreate;
using Campaign.Pooling.Handlers.Seller.InsertSeller;
using Campaign.Pooling.Handlers.SellerManager.GetSellers;
using Campaign.Pooling.Handlers.SellerScore.GetSellersScore;
using Campaign.Pooling.Handlers.UpdateCurrentRevenueSellerManager;
using Campaign.Processor.API.Handlers.RegisterSellerScoreProductResume;
using Campaign.Processor.API.Orchestrators.CalculateScoreByProduct;

namespace Campaign.Pooling.Configurations.ContainerDI.Handlers
{
    public static class HandlersContainerRegister
    {
        public static void AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<IGetSellersHandler, GetSellersHandler>();
            services.AddScoped<IGetSellerScoreHandler, GetSellerScoreHandler>();
            services.AddScoped<IGetOrdersDetailHandler, GetOrdersDetailHandler>();
            services.AddScoped<ICalculateRevenueHandler, CalculateRevenueHandler>();
            services.AddScoped<ICalculateCouponsHandler, CalculateCouponsHandler>();
            services.AddScoped<IInsertSellerScoreHandler, InsertSellerScoreHandler>();
            services.AddScoped<IGetSellersManagersHandler, GetSellersManagersHandler>();
            services.AddScoped<IGetSellersToCreateHandler, GetSellerstoCreateHandler>();
            services.AddScoped<IGetProductsPromotionsHandler, GetProductsPromotionsHandler>();
            services.AddScoped<IProductPromotionExistsHandler, ProductPromotionExistsHandler>();
            services.AddScoped<ICalculateScoreByProductHandler, CalculateScoreByProductHandler>();
            services.AddScoped<ICalculateScoreByProductOrchestrator, CalculateScoreByProductOrchestrator>();
            services.AddScoped<ICalculateRegisteredsConsumersHandler, CalculateRegisteredsConsumersHandler>();
            services.AddScoped<ICalculateReactivatedsConsumersHandler, CalculateReactivatedsConsumersHandler>();
            services.AddScoped<IGetProductPromotionReadDataHistoryHandler, GetProductPromotionReadDataHistoryHandler>();
            services.AddScoped<IRegisterSellerScoreProductSummariesHandler, RegisterSellerScoreProductSummariesHandler>();
            services.AddScoped<IUpdateCurrentRevenueSellerManagerScoreHandler, UpdateCurrentRevenueSellerManagerScoreHandler>();
            services.AddScoped<IRegisterProductPromotionReadDataHistoryHandler, RegisterProductPromotionReadDataHistoryHandler>();
        }
    }
}
