using Campaign.Pooling.Handlers.CalculateScore;
using Campaign.Pooling.Handlers.Seller.GetSellers;
using Campaign.Pooling.Handlers.Seller.InsertSeller;
using Campaign.Pooling.Handlers.SellerManager.GetSellers;
using Campaign.Pooling.Handlers.Seller.GetSellersToCreate;
using Campaign.Pooling.Handlers.Products.GetPromotionsProducts;
using Campaign.Pooling.Handlers.Products.GetLastProductPromotions;

namespace Campaign.Pooling.ContainerDI.Handlers
{
    public static class HandlersInjections
    {
        public static void AddHandlersInjections(this IServiceCollection services)
        {
            services.AddScoped<IGetSellersHandler, GetSellersHandler>();
            services.AddScoped<IInsertSellerScoreHandler, InsertSellerScoreHandler>();
            services.AddScoped<IGetSellersManagersHandler, GetSellersManagersHandler>();
            services.AddScoped<IGetSellersToCreateHandler, GetSellerstoCreateHandler>();
            services.AddScoped<ICalculateScoreOrchestrator, CalculateScoreOrchestrator>();
            services.AddScoped<IGetProductsPromotionsHandler, GetProductsPromotionsHandler>();
            services.AddScoped<IGetLastProductPromotionsHistoryHandler, GetLastProductPromotionsHistoryHandler>();
        }
    }
}
