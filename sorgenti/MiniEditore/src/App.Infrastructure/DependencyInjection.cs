using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using App.Application.Abstractions;
using App.Application.Documenti;
using App.Infrastructure.Data;
using App.Infrastructure.Documenti;
using App.Infrastructure.Background;
using App.Infrastructure.Services;

namespace App.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default") ?? "Data Source=Data/minieditore.db";

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

        services.AddScoped<IDocumentoRepository, DocumentoRepository>();
        services.AddScoped<IDocumentoQueries, DocumentoQueries>();

        services.AddScoped<IClock, SystemClock>();

        services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
        services.AddHostedService<BackgroundJobProcessor>();

        return services;
    }
}
