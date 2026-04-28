using Campaign.API.Handlers.SellerManager.GetRevenue;
using Campaign.API.Handlers.SellerScore.GetSellersScores;
using Campaign.API.Handlers.Supplier;
using Campaign.API.Handlers.Team.Get;
using Campaign.API.Handlers.User.Auth;
using Campaign.API.Handlers.User.GetUser;
using Campaign.API.Handlers.User.InsertScoreByAccess;
using Campaign.API.Handlers.User.RegisterUser;
using Campaign.API.Repositories.Supplier.SupplierProductSold.ReadOnly;

namespace Campaign.API.Configuration.ContainerDI.Handlers
{
    public static class HandlersContainerRegister
    {
        public static void AddHandler(this IServiceCollection services)
        {
            services.AddScoped<IAuthHandler, AuthHandler>();
            services.AddScoped<IGetTeamHandle, GetTeamHandle>();
            services.AddScoped<IGetUserHandler, GetUserHandler>();
            services.AddScoped<IRegisterUserHandler, RegisterUserHandler>();
            services.AddScoped<IGetSellersScoresHnadler, GetSellersScoresHnadler>();
            services.AddScoped<IInsertScoreByAccessHandler, InsertScoreByAccessHandler>();
            services.AddScoped<IGetSupplierProductsSoldHandler, GetSupplierProductsSoldHandler>();
            services.AddScoped<IGetSellerManagerScoreRevenueHandler, GetSellerManagerScoreRevenueHandler>();
            services.AddScoped<ISupplierProductSoldReadOnlyRepository, SupplierProductSoldReadOnlyRepository>();
        }
    }
}
