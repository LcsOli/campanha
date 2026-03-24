using Microsoft.Extensions.DependencyInjection;
using Campaign.Shared.DataBaseContext.Entities.UnityOfWork;

namespace Campaign.Shared.UnitOfWorkDI
{
    public static class UnityOfWorkInjection
    {
        public static void AddUnityOfWorkInjection(this IServiceCollection services)
        {
            services.AddScoped<IUnityOfWork, UnityOfWork>();
        }
    }
}
