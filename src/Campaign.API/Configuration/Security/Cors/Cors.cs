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
                    policy.WithOrigins("http://0.0.0.0:5024",
                                       "http://localhost:8083",
                                       "http://192.168.10.9:8083",
                                       "https://chamados.grupocomprefacil.com.br")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }
    }
}
