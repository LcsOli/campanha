using System.Net;
using Campaign.Shared.Enums.Role;
using Campaign.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Campaign.Program.Register.DTOs;
using Campaign.API.Commands.User.Create;
using Microsoft.Extensions.Configuration;
using Campaign.API.Handlers.User.RegisterUser;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.Shared.DataBaseContext.Entities.UnityOfWork;

namespace Campaign.Program.Register.Sellers
{
    public class UserRegister
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly CampaingContextDb _context;
        private readonly IRegisterUserHandler _registerUserHandler;

        private readonly IConfiguration _configuration = new ConfigurationBuilder()
                                                        .AddJsonFile("SellersToRegister.json")
                                                        .SetBasePath(Directory.GetCurrentDirectory())
                                                        .Build();

        public UserRegister(IUnityOfWork unityOfWork,
                              CampaingContextDb context,
                              IRegisterUserHandler registerUserHandler)
        {
            _context = context;
            _unityOfWork = unityOfWork;
            _registerUserHandler = registerUserHandler;
        }

        public async Task Register()
        {
            var sellersToRegister = _configuration.GetSection("sellers").Get<List<DTOs.UserToRegister>>();

            if (sellersToRegister!.Count <= 0)
                throw new CompaignException(HttpStatusCode.InternalServerError, "Revise os dados do JSON!!!!!!!!!!!!!!!!!!!!!!");

            await SetDocument(sellersToRegister);

            await _unityOfWork.SecureCommitAsync(async () =>
            {
                foreach (var seller in sellersToRegister)
                {
                    try
                    {
                        var cmd = new RegisterUserCommand(Roles.User,
                                                          seller.TeamId,
                                                          seller.Name,
                                                          seller.Id,
                                                          null,
                                                          seller.Document,
                                                          $"{seller.Id}".PadLeft(4, '0'));

                        await _registerUserHandler.Handle(cmd);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"RCA Erro ao cadastrar: {seller.Id} - {ex.Message}");
                    }
                }
            });
        }

        private async Task SetDocument(List<DTOs.UserToRegister> sellersToRegister)
        {

            var sellersIds = sellersToRegister!.Select(x => x.Id).ToArray();

            var sellersDocuments = await _context.Sellers
                                           .Where(x => sellersIds.Contains(x.Id))
                                           .Select(x => new UserDocument(x.Id, x.Document))
                                           .ToListAsync();

            sellersToRegister.ForEach(seller =>
            {
                var sellerDocument = sellersDocuments.SingleOrDefault(x => x.Id == seller.Id);

                if (string.IsNullOrEmpty(sellerDocument!.Document))
                {
                    Console.WriteLine($"RCA Sem documento: {seller.Id}");
                    return;
                }

                var documentWithoutFormatter = sellerDocument.Document.Replace(".", "").Replace("-", "");

                seller.SetDocument(documentWithoutFormatter);
            });
        }
    }
}
