namespace Campaign.API.Configuration.Security
{
    public static class Authorization
    {
        public static void AddAuthorizationConfiguration(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                    .AddPolicy("user", options => options.RequireRole("director", "manager", "user"));
        }
    }
}
