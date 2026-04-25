using App.Api.Endpoints;
using App.Application.DependencyInjection;
using App.Infrastructure;
using App.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Web", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5002",
                "https://localhost:5002",
                "http://localhost:8081")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (IServiceScope scope = app.Services.CreateScope())
{
    AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbInitializer.InitializeAsync(dbContext);
}

app.UseCors("Web");

app.MapGet("/", () => "MiniEditore API");
app.MapHealthChecks("/health/live");

app.MapCatalogoEndpoints();
app.MapClientiEndpoints();
app.MapDocumentiEndpoints();
app.MapDashboardEndpoints();
app.MapReportEndpoints();
app.MapBackgroundEndpoints();

app.Run();

public partial class Program { }
