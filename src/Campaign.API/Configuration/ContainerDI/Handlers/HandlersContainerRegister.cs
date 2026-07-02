using Campaign.API.Handlers.SellerManager.GetRevenue;
using Campaign.API.Handlers.SellerManagerScore.TargetManager;
using Campaign.API.Handlers.SellerScore.GetSellerScoreClientSummary;
using Campaign.API.Handlers.SellerScore.GetSellerScoreProductSummary;
using Campaign.API.Handlers.SellerScore.GetSellersScores;
using Campaign.API.Handlers.SellerScore.RegisterSellerScore;
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
            services.AddScoped<IRegisterSellerScoreHandler, RegisterSellerScoreHandler>();
            services.AddScoped<IGetSupplierProductsSoldHandler, GetSupplierProductsSoldHandler>();
            services.AddScoped<ISellerScoreClientSummaryHandler, SellerScoreClientSummaryHandler>();
            services.AddScoped<ISellerScoreProductSummaryHandler, SellerScoreProductSummaryHandler>();
            services.AddScoped<ISellerManagerRevenueTargetHandler, SellerManagerRevenueTargetHandler>();
            services.AddScoped<ISellerManagerScoreRevenueHandler, SellerManagerScoreRevenueHandler>();
            services.AddScoped<ISupplierProductSoldReadOnlyRepository, SupplierProductSoldReadOnlyRepository>();
        }
    }
}
