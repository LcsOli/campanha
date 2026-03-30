using Microsoft.Extensions.DependencyInjection;
using Campaign.Shared.DataBaseContext.Entities.UnityOfWork;

namespace Campaign.Shared.UnitOfWorkDI
{
    public static class UnitOfWorkContainerRegister
    {
        public static void AddUnityOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnityOfWork, UnityOfWork>();
        }
    }
}
