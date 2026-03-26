using Campaign.Pooling.Orchestrators.UpdateProductPromotionReadHistory;
using Campaign.Pooling.Orchestrators.UpdateSellerScore;

namespace Campaign.Pooling.Configurations.ContainerDI.Orchestrators
{
    public static class OrchestratorsContainerRegister
    {
        public static void AddOrchestrators(this IServiceCollection services)
        {
            services.AddScoped<IUpdateSellerScoreOrchestrator, UpdateSellerScoreOrchestrator>();
            services.AddScoped<IUpdateProductPromotionReadHistoryOrchestrator, UpdateProductPromotionReadHistoryOrchestrator>();
        }
    }
}
