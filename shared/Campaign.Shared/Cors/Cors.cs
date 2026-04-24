using Microsoft.Extensions.DependencyInjection;

namespace Campaign.Shared.cors
{
    public static class Cors
    {
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>            {
                options.AddPolicy("cors", policy =>
                {
                    policy.WithOrigins("http://localhost:8082")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }
    }
}
