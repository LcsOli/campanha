/*
 ATENÇÃO!!!
    - Só rode a aplicação antes de configurar a classe.
    - Os dados serão inseridos diretamente no banco em produção
    - Conferir os arquivos:
        #SellersToRegister.json
        #SellersManagersToRegister.json
        #SuppliersToRegister.json

    - Definir o tipo de usuário que será inserido na propriedade _processType
        - Tipos:
            #Seller
            #SellerManager
            #Supplier
*/

using Campaign.API.Orchestrators.RegisterUser;
using Campaign.Pooling.Commands.Consumers.Get;
using Campaign.Pooling.Configurations.ContainerDI.Handlers;
using Campaign.Pooling.Configurations.ContainerDI.Orchestrators;
using Campaign.Pooling.Configurations.ContainerDI.Repositories;
using Campaign.Pooling.Handlers.CalculateReactivatedsConsummers;
using Campaign.Pooling.Handlers.SellerScore.GetSellersScore;
using Campaign.Pooling.Repositories.OrderDetail.ReadOnly;
using Campaign.Processor.API.Commands.Summaries.Create;
using Campaign.Processor.API.Handlers.RegisterSellerScoreProductSummary;
using Campaign.Program.Register.User;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.Shared.DataBaseContext.Entities.UnityOfWork;
using Campaign.Shared.DataBaseContextDI;
using Campaign.Shared.UnitOfWorkDI;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddDataBase();

services.AddHandlers();
services.AddUnityOfWork();
services.AddRepositories();
services.AddOrchestrators();

var serviceProvider = services.BuildServiceProvider();
var context = serviceProvider.GetService<CampaingContextDb>();

using var scope = serviceProvider.CreateScope();

var getSellerScoreHandler = scope.ServiceProvider.GetRequiredService<IGetSellerScoreHandler>();
var calculateReactivatedsConsumersHandler = scope.ServiceProvider.GetRequiredService<ICalculateReactivatedsConsumersHandler>();

var sellerScore = await getSellerScoreHandler.Handle();

await calculateReactivatedsConsumersHandler.Handle(new CalculateReactivatedsConsumersCommand(202603, sellerScore));









































async Task RegisterSummaryByConsumersReactivateds(int promotionCode)
{
    var orderDetailRepository = scope.ServiceProvider.GetRequiredService<IOrderDetailReadOnlyRepository>();

    var getSellerScoreHandler = scope.ServiceProvider.GetRequiredService<IGetSellerScoreHandler>();
    var registerSummariesHandler = scope.ServiceProvider.GetRequiredService<IRegisterSellerScoreProductSummaryHandler>();

    var sellersScore = await getSellerScoreHandler.Handle();
    var ordersDetails = await orderDetailRepository.GetByPromotionCode(promotionCode);

    await registerSummariesHandler.Handle(new RegisterSellerScoreProductSummaryCommand(promotionCode, ordersDetails, sellersScore));
}


async Task RegisterSummaryBySeles(int promotionCode)
{
    var orderDetailRepository = scope.ServiceProvider.GetRequiredService<IOrderDetailReadOnlyRepository>();

    var getSellerScoreHandler = scope.ServiceProvider.GetRequiredService<IGetSellerScoreHandler>();
    var registerSummariesHandler = scope.ServiceProvider.GetRequiredService<IRegisterSellerScoreProductSummaryHandler>();

    var sellersScore = await getSellerScoreHandler.Handle();
    var ordersDetails = await orderDetailRepository.GetByPromotionCode(promotionCode);

    await registerSummariesHandler.Handle(new RegisterSellerScoreProductSummaryCommand(promotionCode, ordersDetails, sellersScore));
}


async Task UserRegister()
{
    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnityOfWork>();
    var registerUserHandler = scope.ServiceProvider.GetRequiredService<IRegisterUserOrchestrator>();

    var registerSeller = new UserRegister(context!, registerUserHandler);
    await registerSeller.Register();
}