using Campaign.API.Configuration.Container_DI;
using Campaign.API.Configuration.ContainerDI.Handlers;
using Campaign.API.Configuration.ContainerDI.Identity;
using Campaign.API.Configuration.ContainerDI.Repositories;
using Campaign.API.Configuration.Security;
using Campaign.Shared.UnitOfWorkDI;
using Campaign.Shared.DataBaseContextDI;
using Campaign.Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHandlerInjection();
builder.Services.AddIdentityInjection();
builder.Services.AddServicesInjection();
builder.Services.AddDataBaseInjection();
builder.Services.AddRepositoriesInjection();
builder.Services.AddUnityOfWorkInjection();
builder.Services.AddAuthorizationConfiguration();
builder.Services.AddAuthenticationConfigurations();
builder.Services.AddControllerSecurityConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
