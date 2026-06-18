using Campaign.API.Configuration.Container_DI;
using Campaign.API.Configuration.ContainerDI.Handlers;
using Campaign.API.Configuration.ContainerDI.Identity;
using Campaign.API.Configuration.ContainerDI.Orchestrator;
using Campaign.API.Configuration.ContainerDI.Repositories;
using Campaign.API.Orchestrators.RegisterUser;
using Campaign.Program.DataAnalysis;
using Campaign.Program.Register.User;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.Shared.DataBaseContext.Entities.UnityOfWork;
using Campaign.Shared.DataBaseContextDI;
using Campaign.Shared.UnitOfWorkDI;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddDataBase();

services.AddServices();
services.AddIdentity();
services.AddUnityOfWork();
services.AddRepositories();
services.AddOrchestrator();

services.AddHandler();

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

var a = new DataAnalysis(context);
await a.Init();


async Task UserRegister()
{
    using var scope = serviceProvider.CreateScope();
    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnityOfWork>();
    var registerUserHandler = scope.ServiceProvider.GetRequiredService<IRegisterUserOrchestrator>();

    var registerSeller = new UserRegister(context!, registerUserHandler);
    await registerSeller.Register();
}