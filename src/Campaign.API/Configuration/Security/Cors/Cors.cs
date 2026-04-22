namespace Campaign.API.Configuration.Security.Cors
{
    public static class Cors
    {
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("cors", policy =>
                {
                    policy.WithOrigins("http://0.0.0.0:8082")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }
    }
}
