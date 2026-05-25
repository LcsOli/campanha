using Campaign.API.Commands.User.Create;
using Campaign.API.Handlers.User.RegisterUser;
using Campaign.Program.Register.DTOs;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.Shared.Enums.Role;
using Campaign.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Campaign.Program.Register.RCAs
{
    public class SellerRegister
    {
        private CampaingContextDb _context;
        private readonly RegisterUserHandler _registerUserHandler;

        private readonly IConfiguration _configuration = new ConfigurationBuilder()
                                                        .AddJsonFile("RCAs.json")
                                                        .SetBasePath(Directory.GetCurrentDirectory())
                                                        .Build();

        public SellerRegister(CampaingContextDb context,
                              RegisterUserHandler registerUserHandler)
        {
            _context = context;
            _registerUserHandler = registerUserHandler;
        }

        public async Task Register()
        {

            var sellersToRegister = _configuration.GetSection("Sellers").Get<SellersToRegister>();
            /*


            if (sellersToRegister == null || sellersToRegister.Sellers.Count <= 0)
                throw new CompaignException(HttpStatusCode.InternalServerError, "Revise os dados do JSON!!!!!!!!!!!!!!!!!!!!!!");

            await SetDocument(sellersToRegister);

            foreach (var seller in sellersToRegister!.Sellers)
            {
                var cmd = new RegisterUserCommand(Roles.User,
                                                  seller.TeamId,
                                                  seller.Name,
                                                  seller.SellerId,
                                                  null,
                                                  seller.Document,
                                                  seller.Document[..4]);

                await _registerUserHandler.Handle(cmd);
            }
            */
        }

        private async Task SetDocument(SellersToRegister sellersToRegister)
        {

            var sellersIds = sellersToRegister!.Sellers.Select(x => x.SellerId).ToArray();

            var sellersDocuments = await _context.Sellers
                                           .Select(x => new SellersDocument(x.Id, x.Document))
                                           .Where(x => sellersIds.Contains(x.Id))
                                           .ToListAsync();

            sellersToRegister.Sellers.ForEach(seller =>
            {
                var sellerDocument = sellersDocuments.SingleOrDefault(x => x.Id == seller.SellerId);

                if (string.IsNullOrEmpty(sellerDocument!.Document))
                {
                    Console.WriteLine($"RCA Sem documento: {seller.SellerId}");
                    return;
                }

                var documentWithoutFormatter = sellerDocument.Document.Replace(".", "").Replace("-", "");

                seller.SetDocument(documentWithoutFormatter);
            });
        }
    }
}
