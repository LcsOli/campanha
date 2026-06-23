
using Campaign.API.Orchestrators.RegisterUser;
using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Configurations.ContainerDI.Handlers;
using Campaign.Pooling.Configurations.ContainerDI.Orchestrators;
using Campaign.Pooling.Configurations.ContainerDI.Repositories;
using Campaign.Pooling.Handlers.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.SellerScore.GetSellersScore;
using Campaign.Pooling.Repositories.OrderDetail.ReadOnly;
using Campaign.Processor.API.Commands.Summaries.Create;
using Campaign.Processor.API.Handlers.RegisterSellerScoreProductResume;
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

//using var scope = serviceProvider.CreateScope();

//var orderDetailRepository = scope.ServiceProvider.GetRequiredService<IOrderDetailReadOnlyRepository>();
//var calcProductHandler = scope.ServiceProvider.GetRequiredService<ICalculateScoreByProductHandler>();
//var getSellerScoreHandler = scope.ServiceProvider.GetRequiredService<IGetSellerScoreHandler>();

//var sellersScore = await getSellerScoreHandler.Handle();
//var ordersDetails = await orderDetailRepository.GetByPromotionCode(202601);

//calcProductHandler.Handle(new CalculateScoreByProductCommand(202601, ordersDetails, sellersScore));

//await RegisterSummaries(202603);

async Task RegisterSummaries(int promotionCode)
{
    using var scope = serviceProvider.CreateScope();

    var orderDetailRepository = scope.ServiceProvider.GetRequiredService<IOrderDetailReadOnlyRepository>();

    var getSellerScoreHandler = scope.ServiceProvider.GetRequiredService<IGetSellerScoreHandler>();
    var registerSummariesHandler = scope.ServiceProvider.GetRequiredService<IRegisterSellerScoreProductSummariesHandler>();

    var sellersScore = await getSellerScoreHandler.Handle();
    var ordersDetails = await orderDetailRepository.GetByPromotionCode(promotionCode);

    await registerSummariesHandler.Handle(new RegisterSellerScoreProductSummariesCommand(promotionCode, ordersDetails, sellersScore));
}


async Task UserRegister()
{
    using var scope = serviceProvider.CreateScope();
    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnityOfWork>();
    var registerUserHandler = scope.ServiceProvider.GetRequiredService<IRegisterUserOrchestrator>();

    var registerSeller = new UserRegister(context!, registerUserHandler);
    await registerSeller.Register();
}