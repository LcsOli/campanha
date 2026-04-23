
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
                        u.codusur AS "SellerId",
                        u.nome AS "SellerName",
                        cs.nome AS "SellerManagerName",
                        cs.cod_supervisor AS "SellerManagerId"
                    FROM 
                        pcusuari u
                        JOIN cf_campanha_score_supervisores cs ON cs.cod_supervisor = u.codsupervisor
                    WHERE 
                        u.tipovend = 'R' AND
                        u.dttermino IS NULL 

                """).ToListAsync();
        }

        private async Task CreateSellerScore(List<SellersScoreToRegisterModel> sellers)
        {
            //TODO - O id da equipe será definido em Maio. 
            //Atualmente eu estou atribuindo qualquer valor para a equipe a fim de testes. Quando a regra de equipes for definido, vou alterar o algorítimo para colocar o 
            //RCA em equipes específicas.

            await _context.SellerScores.AddRangeAsync(sellers.Select(s => new SellerScore(new Random().Next(1, 4), s.SellerName, s.SellerId, s.SellerManagerName, s.SellerManagerId)));
            await _context.SaveChangesAsync();
        }
    }
}
