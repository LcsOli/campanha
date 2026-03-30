using Campaign.Shared.Middlewares;
using Campaign.Shared.UnitOfWorkDI;
using Campaign.Shared.DataBaseContextDI;
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
