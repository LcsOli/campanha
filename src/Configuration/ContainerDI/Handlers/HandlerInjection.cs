using Campaign.API.Handlers.User.Auth;
using Campaign.API.Handlers.User.GetUser;
using Campaign.API.Handlers.User.RegisterUser;
using Campaign.API.Handlers.User.AuthOrchestrator;

namespace Campaign.API.Configuration.ContainerDI.Handlers
{
    public static class HandlerInjection
    {
        public static void AddHandlerInjection(this IServiceCollection services)
        {
            services.AddScoped<IAuthHandler, AuthHandler>();
            services.AddScoped<IGetUserHandler, GetUserHandler>();
            services.AddScoped<IRegisterUserHandler, RegisterUserHandler>();
            services.AddScoped<IAuthOrchestratorHandler, AuthOrchestratorHandler>();
        }
    }
}
