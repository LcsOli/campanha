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
using Campaign.Pooling.Commands.Calculate;
using Campaign.Pooling.Commands.CalculateScoreByProduct;
using Campaign.Pooling.Configurations.ContainerDI.Handlers;
using Campaign.Pooling.Configurations.ContainerDI.Orchestrators;
using Campaign.Pooling.Configurations.ContainerDI.Repositories;
using Campaign.Pooling.DTO.Response.Order;
using Campaign.Pooling.Handlers.CalculateRevenueTarget;
using Campaign.Pooling.Handlers.CalculateScoreByProduct;
using Campaign.Pooling.Handlers.SellerScore.GetSellersScore;
using Campaign.Pooling.Repositories.Order.ReadOnly;
using Campaign.Pooling.Repositories.OrderProductRemoved.ReadOnly;
using Campaign.Pooling.Repositories.OrderSummary.ReadOnly;
using Campaign.Processor.API.Commands.Summaries.Create;
using Campaign.Processor.API.Handlers.EndOfMonth.ScoreByOrderCanceled;
using Campaign.Processor.API.Handlers.EndOfMonth.ScoreByOrderItemRemoved;
using Campaign.Processor.API.Handlers.RegisterSellerScoreClientSummary;
using Campaign.Processor.API.Handlers.RegisterSellerScoreProductSummary;
using Campaign.Processor.API.Repositories.SellerScoreProductsSummary.ReadOnly;
using Campaign.Processor.API.Repositories.SellerScoreProductsSummary.WriteOnly;
using Campaign.Program.Register.User;
using Campaign.Shared.DataBaseContext.Entities;
using Campaign.Shared.DataBaseContext.Entities.UnityOfWork;
using Campaign.Shared.DataBaseContextDI;
using Campaign.Shared.UnitOfWorkDI;
using Microsoft.Extensions.DependencyInjection;
using StackTraceInternalLibrary.Client;
using StackTraceInternalLibrary.ContainerDI;
using ScoreCanceledCommand = Campaign.Processor.API.Commands.EndOfMonth.ScoreByOrderCanceled.Create;
using ScoreRemovedCommand = Campaign.Processor.API.Commands.EndOfMonth.ScoreByOrderRemoved.Create;

var services = new ServiceCollection();
services.AddDataBase();

services.AddHandlers();
services.AddUnityOfWork();
services.AddRepositories();
services.AddOrchestrators();

services.AddHttpClient<ILogClient, LogClient>();
services.AddHttpContextAccessor();
services.AddStackTraceServices();

var serviceProvider = services.BuildServiceProvider();
var context = serviceProvider.GetService<CampaingContextDb>();

using var scope = serviceProvider.CreateScope();

//await ReprocessAndRegisterProductSummary(202601, 202602, 202603, 202604);
//await CalculateScoreByProductRemoveds(202601, 202602, 202603, 202604);


async Task CalculateScoreByProductRemoveds(params int[] promotionCodes)
{
    var scoreByOrderRemovedHandler = scope.ServiceProvider.GetRequiredService<IScoreByOrderRemovedHandler>();
    var orderDetailReadOnlyRepository = scope.ServiceProvider.GetRequiredService<IOrderDetailReadOnlyRepository>();

    foreach (var promotionCode in promotionCodes)
    {
        var orders = await orderDetailReadOnlyRepository.GetAllByPromotionCode(promotionCode);
        await scoreByOrderRemovedHandler.Handle(new ScoreRemovedCommand.CalculateCommand(promotionCode, orders));
    }
}

async Task CalculateScoreByProductCanceleds(int promotionCode)
{
    var scoreByOrderCanceledHandler = scope.ServiceProvider.GetRequiredService<IScoreByOrderCanceledHandler>();
    var orderDetailReadOnlyRepository = scope.ServiceProvider.GetRequiredService<IOrderDetailReadOnlyRepository>();

    var orders = await orderDetailReadOnlyRepository.GetAllByPromotionCode(promotionCode);

    await scoreByOrderCanceledHandler.Handle(new ScoreCanceledCommand.CalculateCommand(promotionCode, orders));
}
async Task CalculateRevenueOfMonth()
{
    var getSellerScoreHandler = scope.ServiceProvider.GetRequiredService<IGetSellerScoreHandler>();
    var calculateRevenueHandler = scope.ServiceProvider.GetRequiredService<ICalculateRevenueHandler>();

    var promotionCodes = new int[] { 202604 };
    var sellersScore = await getSellerScoreHandler.Handle();

    var sellerScore = sellersScore.Where(x => x.SellerId == 544)
                                    .Select(x =>
                                    {
                                        x.ClearPoints();
                                        return x;
                                    }).ToList();

    foreach (var promotionCode in promotionCodes)
    {
        await calculateRevenueHandler.Handle(new CalculateRevenueMonthCommand(promotionCode, sellerScore));
    }
}

async Task RegisterRegisteredsSummary(int promotionCode)
{
    var getSellerScoreHandler = scope.ServiceProvider.GetRequiredService<IGetSellerScoreHandler>();
    var orderSummaryReadOnlyRepository = scope.ServiceProvider.GetRequiredService<IOrderSummaryReadOnlyRepository>();
    var registerSellerScoreClientSummaryHandler = scope.ServiceProvider.GetRequiredService<IRegisterSellerScoreClientSummaryHandler>();

    var sellerScore = await getSellerScoreHandler.Handle();
    var registereds = await orderSummaryReadOnlyRepository.GetCustomersRegisteredsBySelller(promotionCode);

    await registerSellerScoreClientSummaryHandler.Handle(new RegisterRegisteredsCommand(promotionCode, sellerScore, registereds));
}

async Task RegisterReactivatedsSummary(int promotionCode)
{
    var getSellerScoreHandler = scope.ServiceProvider.GetRequiredService<IGetSellerScoreHandler>();
    var orderSummaryReadOnlyRepository = scope.ServiceProvider.GetRequiredService<IOrderSummaryReadOnlyRepository>();
    var registerSellerScoreClientSummaryHandler = scope.ServiceProvider.GetRequiredService<IRegisterSellerScoreClientSummaryHandler>();

    var sellerScore = await getSellerScoreHandler.Handle();
    var reactivateds = await orderSummaryReadOnlyRepository.GetCustomersReactivatedsBySelller(promotionCode);

    await registerSellerScoreClientSummaryHandler.Handle(new RegisterReactivatedsCommand(promotionCode, sellerScore, reactivateds));
}

async Task RegisterProductSummary(int promotionCode)
{
    var orderDetailRepository = scope.ServiceProvider.GetRequiredService<IOrderDetailReadOnlyRepository>();

    var getSellerScoreHandler = scope.ServiceProvider.GetRequiredService<IGetSellerScoreHandler>();
    var registerSummariesHandler = scope.ServiceProvider.GetRequiredService<IRegisterSellerScoreProductSummaryHandler>();

    var sellersScore = await getSellerScoreHandler.Handle();
    var ordersDetails = await orderDetailRepository.GetByPromotionCode(promotionCode);

    await registerSummariesHandler.Handle(new RegisterSellerScoreProductSummaryCommand(promotionCode, ordersDetails, sellersScore));
}

async Task ReprocessAndRegisterProductSummary(params int[] promotionCodes)
{
    var orderDetailRepository = scope.ServiceProvider.GetRequiredService<IOrderDetailReadOnlyRepository>();
    var orderProductRemovedReadOnlyRepository = scope.ServiceProvider.GetRequiredService<IOrderProductRemovedReadOnlyRepository>();

    var getSellerScoreHandler = scope.ServiceProvider.GetRequiredService<IGetSellerScoreHandler>();
    var registerSummariesHandler = scope.ServiceProvider.GetRequiredService<IRegisterSellerScoreProductSummaryHandler>();

    var sellersScore = await getSellerScoreHandler.Handle();

    foreach (var promotionCode in promotionCodes)
    {
        var ordersDetails = await orderDetailRepository.GetByPromotionCode(promotionCode);

        var ordersIds = ordersDetails.Select(x => x.OrderId).Distinct();
        var productsRemoveds = await orderProductRemovedReadOnlyRepository.GetByOrdersIds(promotionCode, [.. ordersIds]);

        var orders = productsRemoveds.Select(x => new OrderDetailResponse(x.OrderId,
                                                                          x.SellerId,
                                                                          x.ProductId,
                                                                          x.ConsumerId,
                                                                          default!,
                                                                          default!,
                                                                          default!,
                                                                          default!,
                                                                          default!,
                                                                          x.ProductPromotionPoints));

        await registerSummariesHandler.Handle(new RegisterSellerScoreProductSummaryCommand(promotionCode, [.. orders], sellersScore));
    }
}

async Task UserRegister()
{
    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnityOfWork>();
    var registerUserHandler = scope.ServiceProvider.GetRequiredService<IRegisterUserOrchestrator>();

    var registerSeller = new UserRegister(context!, registerUserHandler);
    await registerSeller.Register();
}

async Task CalculateScoreByProduct()
{
    var getSellerScoreHandler = scope.ServiceProvider.GetRequiredService<IGetSellerScoreHandler>();
    var orderDetailReadOnlyRepository = scope.ServiceProvider.GetRequiredService<IOrderDetailReadOnlyRepository>();
    var calculateScoreByProductHandler = scope.ServiceProvider.GetRequiredService<ICalculateScoreByProductHandler>();

    var promotionCodes = new int[] { 202601 };
    var sellersScore = await getSellerScoreHandler.Handle();

    var sellersScores = sellersScore.Where(x => x.SellerId == 522)
                                    .Select(x =>
                                    {
                                        x.ClearPoints();
                                        return x;
                                    }).ToList();
    decimal score = 0;

    foreach (var promotionCode in promotionCodes)
    {
        var ordersDetails = await orderDetailReadOnlyRepository.GetByPromotionCode(promotionCode);
        calculateScoreByProductHandler.Handle(new CalculateScoreByProductCommand(promotionCode, ordersDetails, sellersScores));

        score = sellersScores.First().Score;
    }
}

async Task RemoveSummaryProductsDucplicateds(params int[] promotionsCodes)
{
    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnityOfWork>();
    var sellerScoreProductSummaryReadOnlyRepository = scope.ServiceProvider.GetRequiredService<ISellerScoreProductSummaryReadOnlyRepository>();
    var sellerScoreProductSummaryWriteOnlyRepository = scope.ServiceProvider.GetRequiredService<ISellerScoreProductSummaryWriteOnlyRepository>();

    foreach (var promotionCode in promotionsCodes)
    {
        var products = await sellerScoreProductSummaryReadOnlyRepository.GetByPromotionCode(promotionCode);

        var productsUnits = products.DistinctBy(x => new { x.SellerId, x.ProductId, x.CustomerId }).ToList();
        var productsIds = productsUnits.Select(x => x.Id);

        var productsToRemove = products.Where(x => !productsIds.Contains(x.Id)).ToList();

        sellerScoreProductSummaryWriteOnlyRepository.RemoveRange(productsToRemove);
    }
}
