using Campaign.Pooling.Configurations.ContainerDI.Handlers;
using Campaign.Pooling.Configurations.ContainerDI.Orchestrators;
using Campaign.Pooling.Configurations.ContainerDI.Repositories;
using Campaign.Shared.cors;
using Campaign.Shared.DataBaseContextDI;
using Campaign.Shared.Middlewares;
using Campaign.Shared.UnitOfWorkDI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDataBase();
builder.Services.AddHandlers();
builder.Services.AddUnityOfWork();
builder.Services.AddRepositories();
builder.Services.AddOrchestrators();
builder.Services.AddCorsConfiguration();

//builder.Services.AddHttpClient<ILogClient, LogClient>();
//builder.Services.AddHttpContextAccessor();
//
//builder.Services.AddStackTraceServices();

builder.WebHost.UseUrls("http://0.0.0.0:7168");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors("cors");

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();
//app.UseMiddleware<RequestBodyMiddleware>();

app.Run();