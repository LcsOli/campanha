using Campaign.Pooling.ContainerDI.Repositories;
using Campaign.Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddRepositories();

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
