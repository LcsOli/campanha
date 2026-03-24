using Campaign.API.Configuration.DataBaseContext.UnityOfWork;

namespace Campaign.API.Configuration.ContainerDI.UOW
{
    public static class UnityOfWorkInjection
    {
        public static void AddUnityOfWorkInjecction(this IServiceCollection services)
        {
            services.AddScoped<IUnityOfWork, UnityOfWork>();
        }
    }
}
