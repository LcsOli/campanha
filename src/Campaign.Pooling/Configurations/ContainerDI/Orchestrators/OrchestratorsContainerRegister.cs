using Campaign.Pooling.Orchestrators.MainOrchestrator;
using Campaign.Pooling.Orchestrators.UpdateProductPromotionReadHistory;
using Campaign.Pooling.Orchestrators.UpdateSellerScore;
using Campaign.Processor.API.Orchestrators.CalculateScoreByCustomerSalesEvent;

namespace Campaign.Pooling.Configurations.ContainerDI.Orchestrators
{
    public static class OrchestratorsContainerRegister
    {
        public static void AddOrchestrators(this IServiceCollection services)
        {
            services.AddScoped<IMainOrchestrator, MainOrchestrator>();
            services.AddScoped<ICalculateScoreOrchestrator, CalculateScoreOrchestrator>();
            services.AddScoped<IUpdateProductPromotionReadHistoryOrchestrator, UpdateProductPromotionReadHistoryOrchestrator>();
            services.AddScoped<ICalculateScoreByCustomerSalesEventOrchestrator, CalculateScoreByCustomerSalesEventOrchestrator>();
        }
    }
}
