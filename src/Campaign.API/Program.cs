using Campaign.Shared.cors;
using Campaign.Shared.Middlewares;
using Campaign.Shared.UnitOfWorkDI;
using Campaign.Shared.DataBaseContextDI;
using Campaign.API.Configuration.Container_DI;
using Campaign.API.Configuration.Security.Auth;
using Campaign.API.Configuration.ContainerDI.Handlers;
using Campaign.API.Configuration.ContainerDI.Identity;
using Campaign.API.Configuration.ContainerDI.Repositories;
using Campaign.API.Configuration.ContainerDI.Orchestrator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHandler();
builder.Services.AddIdentity();
builder.Services.AddServices();
builder.Services.AddDataBase();
builder.Services.AddUnityOfWork();
builder.Services.AddOrchestrator();
builder.Services.AddRepositories();
builder.Services.AddCorsConfiguration();
builder.Services.AddAuthorizationConfiguration();
builder.Services.AddAuthenticationConfigurations();
builder.Services.AddControllerSecurityConfiguration();

//builder.Services.AddHttpClient<ILogClient, LogClient>();
//builder.Services.AddHttpContextAccessor();

//builder.Services.AddStackTraceServices();

builder.WebHost.UseUrls("http://0.0.0.0:7025");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("cors");

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();
//app.UseMiddleware<RequestBodyMiddleware>();

app.Run();