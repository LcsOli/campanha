using Campaign.Program.Register.DTOs;
using Campaign.Shared.DataBaseContext.Entities;
using Microsoft.Extensions.Configuration;

namespace Campaign.Program.DataAnalysis
{
    public class DataAnalysis
    {
        private readonly CampaingContextDb _context;

        private readonly IConfiguration _configuration = new ConfigurationBuilder()
                                                .AddJsonFile("DataToAnalyse.json")
                                                .SetBasePath(Directory.GetCurrentDirectory())
                                                .Build();

        public DataAnalysis(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task Init()
        {
            var data_1 = _configuration.GetSection("data_1").Get<List<DataToAnalysis>>();
            var data_2 = _configuration.GetSection("data_2").Get<List<DataToAnalysis>>();

            var result = data_1.Intersect(data_2).ToList();

            var validation = result.Where(x => x.LastPointByAccess.Date < new DateTime(2026, 06, 14).Date).ToList();

        }
    }
}
