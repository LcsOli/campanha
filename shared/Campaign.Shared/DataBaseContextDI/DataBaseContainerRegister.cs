using System.Net;
using Campaign.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Campaign.Shared.DataBaseContext.Entities;

namespace Campaign.Shared.DataBaseContextDI
{
    public static class DataBaseContainerRegister
    {
        public static void AddDataBase(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("CAMPAIGN_HOMOLOGA_CONNECTION") ??
                throw new CompaignException(HttpStatusCode.InternalServerError, "Variável de ambient CAMPAIGN_HOMOLOGA_CONNECTION não encontrada.");

            services.AddDbContext<CampaingContextDb>(options => options.UseOracle(connectionString,
                                                     options => options.MigrationsHistoryTable("CF_EF_HISTORICO_DE_MIGRACOES")));

        }
    }
}
