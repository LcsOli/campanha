using Campaign.Pooling.Orchestrators.UpdateProductPromotionReadHistory;

namespace Campaign.Pooling.Configurations.ContainerDI.Orchestrators
{
    public static class OrchestratorsContainerRegister
    {
        public static void AddOrchestrators(this IServiceCollection services)
        {
            services.AddScoped<IUpdateProductPromotionReadHistoryOrchestrator, UpdateProductPromotionReadHistoryOrchestrator>();
        }
    }
}
