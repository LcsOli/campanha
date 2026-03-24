using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Campaign.API.Configuration.Security
{
    public static class ControllerSecurityConfiguration
    {
        public static void AddControllerSecurityConfiguration(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            });
        }
    }
}
