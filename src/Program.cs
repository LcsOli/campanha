using Campaign.API.Configuration.Security;
using Campaign.API.Configuration.Container_DI;
using Campaign.API.Configuration.Container_DI.DataBaseContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddServicesInject();
builder.Services.AddDataBaseInjection();
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

app.Run();
