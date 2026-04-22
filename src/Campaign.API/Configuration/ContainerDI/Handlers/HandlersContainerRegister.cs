using Campaign.API.Handlers.SellerManager.GetRevenue;
using Campaign.API.Handlers.User.Auth;
using Campaign.API.Handlers.User.GetUser;
using Campaign.API.Handlers.User.InsertScoreByAccess;
using Campaign.API.Handlers.User.RegisterUser;

namespace Campaign.API.Configuration.ContainerDI.Handlers
{
    public static class HandlersContainerRegister
    {
        public static void AddHandler(this IServiceCollection services)
        {
            services.AddScoped<IAuthHandler, AuthHandler>();
            services.AddScoped<IGetUserHandler, GetUserHandler>();
            services.AddScoped<IRegisterUserHandler, RegisterUserHandler>();
            services.AddScoped<IInsertScoreByAccessHandler, InsertScoreByAccessHandler>();
            services.AddScoped<IGetSellerManagerScoreRevenueHandler, GetSellerManagerScoreRevenueHandler>();
        }
    }
}
