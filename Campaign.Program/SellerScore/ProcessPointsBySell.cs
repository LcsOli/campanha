using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.SellerScore.GetSellersScore;
using Campaign.Pooling.Repositories.OrderDetail.ReadOnly;
using Campaign.Pooling.Repositories.SellerScore.ReadOnly;
using Campaign.Shared.DataBaseContext.Entities;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Campaign.Program.SellerScore
{
    public class ProcessPointsBySell
    {
        private readonly int _year = 2025;

        private readonly CampaingContextDb _context;
        public ProcessPointsBySell(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task Process()
        {
            File.WriteAllText(@$"C:\Users\matheusp\Desktop\Campanha_2026\CSVs\{DateTime.Now:yyyy-MM-dd}.csv", "Teste;aaa;123");

            return;
            var sellerScoreReadOnlyRepository = new SellerScoreReadOnlyRepository(_context);
            var getSellerScoreHandler = new GetSellerScoreHandler(sellerScoreReadOnlyRepository);

            var sellersScore = await getSellerScoreHandler.Handle();

            //sellersScore = [.. sellersScore.Where(s => s.SellerId == 1893)];
            sellersScore.ForEach(s => s.UpdateScore(s.Score * -1));

            var orderDetailReadOnlyRepository = new OrderDetailReadOnlyRepository(_context);
            var calculateScoreByProductHandler = new CalculateScoreByProductHandler(orderDetailReadOnlyRepository);

            var promotionsCodes = await ProductsPromotions();

            foreach (var promotionCode in promotionsCodes)
            {

                var cmd = new CalculateScoreByProductCommand(promotionCode, sellersScore);
                await calculateScoreByProductHandler.Handle(cmd);

                //PrintScore(sellersScore, promotionCode);
                CreateCsv(sellersScore, promotionCode);
            }
        }

        private async Task<List<int>> ProductsPromotions()
        {
            var productsPromotions = await _context.ProductPromotionSummaries.Where(p => p.InitIn.Date.Year == _year)
                                                   .OrderBy(p => p.Id)
                                                   .ToListAsync();
            productsPromotions.RemoveAt(0);

            return [.. productsPromotions.Select(p => p.Id)];
        }

        public static void PrintScore(List<Entity.SellerScore> sellersScores, int promotionCode)
        {
            var builder = new StringBuilder();

            sellersScores.ForEach(s =>
                builder.AppendLine($"Seller: {s.Name} - Score by sell: {s.Score}"));

            Console.WriteLine(promotionCode);
            Console.WriteLine();
            Console.WriteLine(builder.ToString());
            Console.WriteLine();
        }

        public static void CreateCsv(List<Entity.SellerScore> sellersScores, int promotionCode)
        {
            var builder = new StringBuilder();
            builder.AppendLine("SellerId;SellerName;ScoreBySell");
            
            sellersScores.OrderBy(s => s.Score)
                         .ToList()
                         .ForEach(s => builder.AppendLine($"{s.SellerId};{s.Name};{s.Score}"));

            File.WriteAllText($"ScoreBySell_{promotionCode}.csv", builder.ToString(), Encoding.UTF8);
        }
    }
}
