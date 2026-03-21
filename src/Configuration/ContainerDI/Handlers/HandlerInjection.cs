using Campaign.API.Handlers.User.Auth;
using Campaign.API.Handlers.User.RegisterUser;

namespace Campaign.API.Configuration.ContainerDI.Handlers
{
    public static class HandlerInjection
    {
        public static void AddHandlerInjection(this IServiceCollection services)
        {
            services.AddScoped<IAuthHandler, AuthHandler>();
            services.AddScoped<IRegisterUserHandler, RegisterUserHandler>();
        }
    }
}
