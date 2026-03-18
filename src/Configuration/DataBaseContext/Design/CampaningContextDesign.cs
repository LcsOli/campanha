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
            var environment = Environment.GetEnvironmentVariable("CAMPAIGN_HOMOLOGA_DB") ??
                throw new CompaignException(HttpStatusCode.InternalServerError, "Variável de ambient CAMPAIGN_HOMOLOGA_CONNECTION não encontrada.");

            var options = new DbContextOptionsBuilder<CampaingContextDb>().UseOracle(environment);

            return new CampaingContextDb(options.Options);
        }
    }
}
