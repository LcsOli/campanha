using Campaign.API.Commands.User.Create;
using Campaign.API.Orchestrators.RegisterUser;
using Campaign.Program.Register.DTOs;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.Shared.Enums.Role;
using Campaign.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Campaign.Program.Register.User
{
    public enum UserType
    {
        Seller,
        SellerManager,
        Supplier
    }

    public class UserRegister
    {
        private readonly CampaingContextDb _context;
        private readonly IRegisterUserOrchestrator _registerUserOrchestrator;

        private readonly static UserType _processType = UserType.Supplier;

        private readonly static string _jsonFilePath = _processType switch
        {
            UserType.Seller => "SellersToRegister.json",
            UserType.SellerManager => "SellersManagersToRegister.json",
            UserType.Supplier => "SuppliersToRegister.json",
            _ => throw new NotImplementedException()
        };


        private readonly IConfiguration _configuration = new ConfigurationBuilder()
                                                        .AddJsonFile(_jsonFilePath)
                                                        .SetBasePath(Directory.GetCurrentDirectory())
                                                        .Build();

        public UserRegister(CampaingContextDb context,
                            IRegisterUserOrchestrator registerUserOrchestrator)
        {
            _context = context;
            _registerUserOrchestrator = registerUserOrchestrator;
        }

        public async Task Register()
        {
            var userToRegistry = _configuration.GetSection("users").Get<List<DTOs.UserToRegister>>();

            if (userToRegistry!.Count <= 0)
                throw new CompaignException(HttpStatusCode.InternalServerError, "Revise os dados do JSON!!!!!!!!!!!!!!!!!!!!!!");

            switch (_processType)
            {
                case UserType.Seller:
                    await RegisterSeller(userToRegistry!);
                    break;
                case UserType.SellerManager:
                    await RegisterSellerManager(userToRegistry!);
                    break;
                default:
                    await RegisterSupplier(userToRegistry!);
                    break;
            }
        }

        private async Task RegisterSupplier(List<UserToRegister> userToRegistry)
        {
            await SetDocument(userToRegistry, UserType.Supplier);

            foreach (var supplier in userToRegistry)
            {
                try
                {
                    var cmd = new RegisterUserCommand(Roles.Supplier,
                                                      null,
                                                      supplier.Name,
                                                      null,
                                                      supplier.Id,
                                                      supplier.Document,
                                                      supplier.Document[..4],
                                                      null,
                                                      null);

                    await _registerUserOrchestrator.Execute(cmd);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao cadastrar: {supplier.Id} - {ex.Message}");
                }
            }
        }

        private async Task RegisterSellerManager(List<UserToRegister> userToRegistry)
        {
            foreach (var seller in userToRegistry)
            {
                try
                {
                    var cmd = new RegisterUserCommand(Roles.Manager,
                                                      null,
                                                      seller.Name,
                                                      seller.Id,
                                                      null,
                                                      seller.Document,
                                                      $"{seller.Id}".PadLeft(4, '0'),
                                                      null,
                                                      null);

                    await _registerUserOrchestrator.Execute(cmd);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao cadastrar: {seller.Id} - {ex.Message}");
                }
            }
        }

        private async Task RegisterSeller(List<UserToRegister> userToRegistry)
        {
            await SetDocument(userToRegistry, UserType.Seller);
            await SetSellerManager(userToRegistry);

            foreach (var seller in userToRegistry)
            {
                try
                {
                    var cmd = new RegisterUserCommand(Roles.User,
                                                      seller.TeamId,
                                                      seller.Name,
                                                      seller.Id,
                                                      null,
                                                      seller.Document,
                                                      $"{seller.Id}".PadLeft(4, '0'),
                                                      seller.SellerManagerId,
                                                      seller.SellerMangerName);

                    await _registerUserOrchestrator.Execute(cmd);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao cadastrar: {seller.Id} - {ex.Message}");
                }
            }
        }

        private async Task SetDocument(List<UserToRegister> userToRegistry, UserType userType)
        {
            switch (userType)
            {
                case UserType.Seller:
                    await SetSellerDocument(userToRegistry);
                    break;
                case UserType.Supplier:
                    await SetSupplierDocument(userToRegistry);
                    break;
            }
        }

        private async Task SetSupplierDocument(List<UserToRegister> userToRegistry)
        {
            var suppliersIds = userToRegistry!.Select(x => x.Id).ToArray();

            var suppliersDocuments = await _context.Suppliers
                                                   .Where(x => suppliersIds.Contains(x.Id))
                                                   .Select(x => new UserDocument(x.Id, x.Document))
                                                   .ToListAsync();

            userToRegistry.ForEach(user =>
            {
                var supplierDocument = suppliersDocuments.SingleOrDefault(x => x.Id == user.Id);

                if (string.IsNullOrEmpty(supplierDocument!.Document))
                {
                    Console.WriteLine($"Usuário Sem documento: {user.Id}");
                    return;
                }

                var documentWithoutFormatter = supplierDocument.Document.Replace(".", "").Replace("-", "");

                user.SetDocument(documentWithoutFormatter);
            });
        }

        private async Task SetSellerDocument(List<UserToRegister> userToRegistry)
        {

            var sellersIds = userToRegistry!.Select(x => x.Id).ToArray();

            var sellersDocuments = await _context.Sellers
                                           .Where(x => sellersIds.Contains(x.Id))
                                           .Select(x => new UserDocument(x.Id, x.Document))
                                           .ToListAsync();

            userToRegistry.ForEach(user =>
            {
                var sellerDocument = sellersDocuments.SingleOrDefault(x => x.Id == user.Id);

                if (string.IsNullOrEmpty(sellerDocument!.Document))
                {
                    Console.WriteLine($"Usuário Sem documento: {user.Id}");
                    return;
                }

                var documentWithoutFormatter = sellerDocument.Document.Replace(".", "").Replace("-", "");

                user.SetDocument(documentWithoutFormatter);
            });
        }

        private async Task SetSellerManager(List<UserToRegister> sellersToRegister)
        {
            var sellerMangersIds = sellersToRegister!.Select(x => x.SellerManagerId).ToArray();

            var sellersManagersInfos = await _context.SellerManagers
                                           .Where(x => sellerMangersIds.Contains(x.Code))
                                           .Select(x => new SellerMangerInfos(x.Code, x.Name))
                                           .ToListAsync();


            sellersToRegister.ForEach(seller =>
            {
                var sellerManger = sellersManagersInfos.SingleOrDefault(x => x.Id == seller.SellerManagerId);

                if (string.IsNullOrEmpty(sellerManger!.Name))
                {
                    Console.WriteLine($"Gerente do vendedor {seller.Id} não encontrado.");
                    return;
                }

                seller.SetSellerManagerName(sellerManger!.Name);
            });

        }
    }
}
