using Campaign.API.Handlers.User.Auth;
using Campaign.API.Handlers.User.GetUser;
using Campaign.API.Handlers.User.RegisterUser;
using Campaign.API.Handlers.User.AuthOrchestrator;
using Campaign.API.Handlers.User.InsertScoreByAccess;

namespace Campaign.API.Configuration.ContainerDI.Handlers
{
    public static class HandlersContainerRegister
    {
        public static void AddHandler(this IServiceCollection services)
        {
            services.AddScoped<IAuthHandler, AuthHandler>();
            services.AddScoped<IGetUserHandler, GetUserHandler>();
            services.AddScoped<IRegisterUserHandler, RegisterUserHandler>();
            services.AddScoped<IAuthOrchestrator, AuthOrchestrator>();
            services.AddScoped<IInsertScoreByAccessHandler, InsertScoreByAccessHandler>();
        }
    }
}
