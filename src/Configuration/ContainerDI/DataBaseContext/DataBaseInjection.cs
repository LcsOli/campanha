using System.Net;
using Microsoft.EntityFrameworkCore;
using Campaign.API.Configuration.Exceptions;
using Campaign.API.Configuration.DataBaseContext;

namespace Campaign.API.Configuration.Container_DI.DataBaseContext
{
    public static class DataBaseInjection
    {
        public static void AddDataBaseInjection(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("CAMPAIGN_HOMOLOGA_CONNECTION") ??
                throw new CompaignException(HttpStatusCode.InternalServerError, "Variável de ambient CAMPAIGN_HOMOLOGA_CONNECTION não encontrada.");

            services.AddDbContext<CampaingContextDb>(options => options.UseOracle(connectionString,
                                                                          options => options.MigrationsHistoryTable("CF_EF_HISTORICO_DE_MIGRACOES")));
        }
    }
}
