using Campaign.API.Background;
using Campaign.API.Configuration.Container_DI;
using Campaign.API.Configuration.Container_DI.DataBaseContext;
using Campaign.API.Configuration.ContainerDI.Handlers;
using Campaign.API.Configuration.ContainerDI.Identity;
using Campaign.API.Configuration.ContainerDI.Repositories;
using Campaign.API.Configuration.ContainerDI.UOW;
using Campaign.API.Configuration.Middlewares;
using Campaign.API.Configuration.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHostedService<TestBackgroundService>();

builder.Services.AddHandlerInjection();
builder.Services.AddIdentityInjection();
builder.Services.AddServicesInjection();
builder.Services.AddDataBaseInjection();
builder.Services.AddRepositoriesInjection();
builder.Services.AddUnityOfWorkInjecction();
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
