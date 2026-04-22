namespace Campaign.API.Configuration.Security.Auth
{
    public static class Authorization
    {
        public static void AddAuthorizationConfiguration(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                    .AddPolicy("user", options => options.RequireRole("supplier", "manager", "user"));
        }
    }
}
