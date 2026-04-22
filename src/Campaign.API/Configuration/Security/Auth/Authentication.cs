using Microsoft.IdentityModel.Tokens;
using Campaign.API.Services.SecretKey;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Campaign.API.Configuration.Security.Auth
{
    public static class Authentication
    {
        public static void AddAuthenticationConfigurations(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(SecretKeyService.GetBytes())
                };
            });
        }
    }
}
