using Campaign.Shared.Middlewares;
using Campaign.Pooling.ContainerDI.Handlers;
using Campaign.Pooling.ContainerDI.Repositories;
using Campaign.Pooling.ContainerDI.Orchestrators;

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
