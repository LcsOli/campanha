using Campaign.API.Orchestrators.Auth;
using Campaign.API.Orchestrators.RegisterUser;

namespace Campaign.API.Configuration.ContainerDI.Orchestrator
{
    public static class OrchestratorContainerRegister
    {
        public static void AddOrchestrator(this IServiceCollection services)
        {
            services.AddScoped<IAuthOrchestrator, AuthOrchestrator>();
            services.AddScoped<IRegisterUserOrchestrator, RegisterUserOrchestrator>();
        }
    }
}
