using System.Text;
using Campaign.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.CalculateScoreByProduct;
using Campaign.Pooling.Repositories.OrderDetail.ReadOnly;
using Campaign.Pooling.Repositories.SellerScore.ReadOnly;
using Campaign.Pooling.Repositories.OrderSummary.ReadOnly;
using Campaign.Pooling.Handlers.SellerScore.GetSellersScore;
using Campaign.Pooling.Handlers.CalculateRegisteredsConsummers;
using Entity = Campaign.Shared.DataBaseContext.Entities.Seller;
using Campaign.Pooling.Handlers.CalculateReactivatedsConsummers;

namespace Campaign.Program.SellerScore
{
    public class ProcessPointsBySell
    {
        private readonly int _year = 2026;

        private readonly CampaingContextDb _context;
        public ProcessPointsBySell(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task Process()
        {
            var sellerScoreReadOnlyRepository = new SellerScoreReadOnlyRepository(_context);
            var getSellerScoreHandler = new GetSellerScoreHandler(sellerScoreReadOnlyRepository);

            var promotionsCodes = await ProductsPromotions();
            var sellersScore = await getSellerScoreHandler.Handle();

            var sellersToFind = new int[] { 1901 };

            sellersScore = sellersScore.Where(s => sellersToFind.Contains(s.SellerId)).ToList();

            sellersScore.ForEach(s =>
            {
                s.UpdateScore(s.Score * -1);
                s.UpdateQtyRegistereds((short)(s.QtyConsumersRegistereds * -1));
                s.UpdateQtyReactivateds((short)(s.QtyConsumersReactivateds * -1));
            });

            await CalculateScore(sellersScore, promotionsCodes);
        }

        private async Task CalculateScore(List<Entity.SellerScore> sellersScore, List<int> promotionsCodes)
        {

            var orderDetailReadOnlyRepository = new OrderDetailReadOnlyRepository(_context);
            var orderSummaryReadOnlyRepository = new OrderSummaryReadOnlyRepository(_context);

            //var calculateScoreByProductHandler = new CalculateScoreByProductHandler(orderDetailReadOnlyRepository);
            var calculateRegisteredsConsumersHandler = new CalculateRegisteredsConsumersHandler(orderSummaryReadOnlyRepository);
            var calculateReactivatedsConsumersHandler = new CalculateReactivatedsConsumersHandler(orderSummaryReadOnlyRepository);

            foreach (var promotionCode in promotionsCodes)
            {

                Console.WriteLine("Step.1");
                //await calculateScoreByProductHandler.Handle(new CalculateScoreByProductCommand(promotionCode, sellersScore));

                //Console.WriteLine("Step.2");
                //await calculateRegisteredsConsumersHandler.Handle(new CalculateRegisteredsConsumersCommand(promotionCode, sellersScore));

                //Console.WriteLine("Step.3");
                //await calculateReactivatedsConsumersHandler.Handle(new CalculateReactivatedsConsumersCommand(promotionCode, sellersScore));

                //CreateCsv(sellersScore, promotionCode);
            }
        }

        private async Task<List<int>> ProductsPromotions()
        {
            var productsPromotions = await _context.ProductPromotionSummaries
                                                   .Where(p => p.InitIn.Date.Year == _year)
                                                   .OrderBy(p => p.Id)
                                                   .ToListAsync();
            productsPromotions.RemoveAt(0);

            return [.. productsPromotions.Select(p => p.Id)];
        }

        public static void CreateCsv(List<Entity.SellerScore> sellersScores, int promotionCode)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Seller_Id;Seller_Name;Score;Score_By_Sell;Score_By_Reactivateds;Score_By_Registereds;Qty_Reactivateds;Qty_Registereds;");

            sellersScores.OrderByDescending(s => s.Score)
                         .ToList()
                         .ForEach(s =>
                         {
                             var pointsByRegistereds = s.QtyConsumersRegistereds * 1000;
                             var pointsByreactivateds = s.QtyConsumersReactivateds * 1000;

                             var scoreBySell = s.Score - (pointsByRegistereds + pointsByreactivateds);

                             builder.AppendLine($"{s.SellerId};{s.Name};{s.Score};{scoreBySell};{s.QtyConsumersReactivateds};{s.QtyConsumersRegistereds};{pointsByreactivateds};{pointsByRegistereds}");
                         });

            var csvService = new CsvService("Tests", $"ScoreBySell_{promotionCode}.csv");
            csvService.Create(builder.ToString());

            Console.WriteLine(promotionCode);
        }
    }
}
