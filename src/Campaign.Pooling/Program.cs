using StackTraceLib.Middleware;
using Campaign.Shared.Middlewares;
using Campaign.Shared.UnitOfWorkDI;
using StackTraceInternalLibrary.Client;
using Campaign.Shared.DataBaseContextDI;
using StackTraceInternalLibrary.ContainerDI;
using Campaign.Pooling.Configurations.ContainerDI.Handlers;
using Campaign.Pooling.Configurations.ContainerDI.Repositories;
using Campaign.Pooling.Configurations.ContainerDI.Orchestrators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDataBase();
builder.Services.AddHandlers();
builder.Services.AddUnityOfWork();
builder.Services.AddRepositories();
builder.Services.AddOrchestrators();

builder.Services.AddHttpClient<ILogClient, LogClient>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddStackTraceServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RequestBodyMiddleware>();

app.Run();