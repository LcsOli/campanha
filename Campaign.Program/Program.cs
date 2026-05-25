

using Campaign.API.Handlers.User.RegisterUser;
using Campaign.Program.Register.RCAs;
using Campaign.Program.SellerScore;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.Shared.DataBaseContextDI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var services = new ServiceCollection();
services.AddDataBase();

var serviceProvider = services.BuildServiceProvider();

var teste = Directory.GetCurrentDirectory();

var context = serviceProvider.GetService<CampaingContextDb>();


//var sellerScoreCreator = new SellerScoreCreator(context);
//await sellerScoreCreator.Create();

//var processPointsBySell = new ProcessPointsBySell(context);
//await processPointsBySell.Process();



var registerSeller = new SellerRegister(context, serviceProvider.GetService<RegisterUserHandler>());
await registerSeller.Register();