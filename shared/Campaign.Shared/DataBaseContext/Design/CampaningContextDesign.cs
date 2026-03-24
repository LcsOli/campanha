using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Campaign.API.Configuration.Exceptions;

namespace Campaign.API.Configuration.DataBaseContext.Design
{
    public class CampaningContextDesign : IDesignTimeDbContextFactory<CampaingContextDb>
    {
        public CampaingContextDb CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("CAMPAIGN_HOMOLOGA_CONNECTION") ??
                throw new CompaignException(HttpStatusCode.InternalServerError, "Variável de ambient CAMPAIGN_HOMOLOGA_CONNECTION não encontrada.");

            var options = new DbContextOptionsBuilder<CampaingContextDb>().UseOracle(connectionString, 
                                                                          options => options.MigrationsHistoryTable("CF_EF_HISTORICO_DE_MIGRACOES"));

            return new CampaingContextDb(options.Options);
        }
    }
}
