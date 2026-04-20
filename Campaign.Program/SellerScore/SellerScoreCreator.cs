
using Campaign.Program.Models;
using Microsoft.EntityFrameworkCore;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.Shared.DataBaseContext.Entities.Seller;

namespace Campaign.Program.CreateSellersScore
{
    public class SellerScoreCreator
    {
        private readonly CampaingContextDb _context;
        public SellerScoreCreator(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task Create()
        {
            var sellersScore = await SellersToRegister();
            await CreateSellerScore(sellersScore);

        }

        private async Task<List<SellersScoreToRegisterModel>> SellersToRegister()
        {
            return await _context.Database.SqlQuery<SellersScoreToRegisterModel>($"""

                    SELECT 
                       u.codusur AS SellerId,
                       u.nome AS SellerName,
                       cs.nome AS  SellerManagerName
                    FROM 
                       pcusuari u
                       join cf_campanha_supervisores cs on cs.cod_supervisor = u.codsupervisor
                    WHERE 
                       u.tipovend = 'R' and
                       u.dttermino is null;

                """).ToListAsync();
        }

        private async Task CreateSellerScore(List<SellersScoreToRegisterModel> sellers)
        {
            //TODO - O id da equipe será definido em Maio. 
            //Atualmente eu estou atribuindo qualquer valor para a equipe a fim de testes. Quando a regra de equipes for definido, vou alterar o algorítimo para colocar o 
            //RCA em equipes específicas.

           await _context.SellerScores.AddRangeAsync(sellers.Select(s => new SellerScore(new Random().Next(1, 4), s.SellerName, s.SellerId, s.SellerManagerName)));
           await _context.SaveChangesAsync();
        }
    }
}
