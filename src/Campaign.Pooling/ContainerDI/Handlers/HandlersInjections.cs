using Campaign.Pooling.Handlers.Products.GetLastProductPromotions;
using Campaign.Pooling.Handlers.Products.GetPromotionsProducts;
using Campaign.Pooling.Handlers.Seller.CreateSeller;
using Campaign.Pooling.Handlers.Seller.GetSellersToCreate;

namespace Campaign.Pooling.ContainerDI.Handlers
{
    public static class HandlersInjections
    {
        public static void AddHandlersInjections(this IServiceCollection services)
        {
            services.AddScoped<IInsertSellerScoreHandler, InsertSellerScoreHandler>();
            services.AddScoped<IGetSellerstoCreateHandler, GetSellerstoCreateHandler>();
            services.AddScoped<IGetProductsPromotionsHandler, GetProductsPromotionsHandler>();
            services.AddScoped<IGetLastProductPromotionsHandler, GetLastProductPromotionsHandler>();
        }
    }
}
