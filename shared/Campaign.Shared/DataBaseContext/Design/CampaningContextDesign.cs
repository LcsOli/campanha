using System.Net;
using Campaign.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Campaign.Shared.DataBaseContext.Entities;

namespace Campaign.Shared.DataBaseContext.Design
{
    public class CampaningContextDesign : IDesignTimeDbContextFactory<CampaingContextDb>
    {
        public CampaingContextDb CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("DATABASE_WINT") ??
                throw new CompaignException(HttpStatusCode.InternalServerError, "Variável de ambient DATABASE_WINT não encontrada.");

            var options = new DbContextOptionsBuilder<CampaingContextDb>().UseOracle(connectionString, 
                                                                          options => options.MigrationsHistoryTable("CF_EF_CAMPANHA_HISTORICO_DE_MIGRACOES"));

            return new CampaingContextDb(options.Options);
        }
    }
}
