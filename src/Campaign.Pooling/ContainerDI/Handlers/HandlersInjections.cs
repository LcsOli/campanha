using Campaign.Pooling.Handlers.Products.GetLastProductPromotions;
using Campaign.Pooling.Handlers.Products.GetPromotionsProducts;

namespace Campaign.Pooling.ContainerDI.Handlers
{
    public static class HandlersInjections
    {
        public static void AddHandlersInjections(this IServiceCollection services)
        {
            services.AddScoped<IGetProductsPromotionsHandler, GetProductsPromotionsHandler>();
            services.AddScoped<IGetLastProductPromotionsHandler, GetLastProductPromotionsHandler>();
        }
    }
}
