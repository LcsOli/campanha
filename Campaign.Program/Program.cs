using Campaign.Shared.UnitOfWorkDI;
using Campaign.Program.Register.Sellers;
using Campaign.Shared.DataBaseContextDI;
using Campaign.API.Configuration.Container_DI;
using Campaign.API.Handlers.User.RegisterUser;
using Microsoft.Extensions.DependencyInjection;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.API.Configuration.ContainerDI.Handlers;
using Campaign.API.Configuration.ContainerDI.Identity;
using Campaign.API.Configuration.ContainerDI.Repositories;
using Campaign.Shared.DataBaseContext.Entities.UnityOfWork;
using Campaign.API.Orchestrators.RegisterUser;
using DocumentFormat.OpenXml.Office2013.Excel;
using Campaign.API.Configuration.ContainerDI.Orchestrator;


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

async Task UserRegister()
{
    using (var scope = serviceProvider.CreateScope())
    {
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnityOfWork>();
        var registerUserHandler = scope.ServiceProvider.GetRequiredService<IRegisterUserOrchestrator>();

        var registerSeller = new UserRegister(unitOfWork, context!, registerUserHandler);
        await registerSeller.Register();
    }
}