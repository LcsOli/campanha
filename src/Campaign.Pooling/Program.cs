using Campaign.Shared.Middlewares;
using Campaign.Pooling.Configurations.ContainerDI.Handlers;
using Campaign.Pooling.Configurations.ContainerDI.Orchestrators;
using Campaign.Pooling.Configurations.ContainerDI.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHandlers();
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
