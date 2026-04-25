using Microsoft.Extensions.DependencyInjection;
using App.Application.Catalogo.Libri;
using App.Application.Catalogo.Autori;
using App.Application.Catalogo.Collane;
using App.Application.Clienti;
using App.Application.Dashboard;
using App.Application.Report;

namespace App.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ILibroService, LibroService>();
        services.AddScoped<IAutoreService, AutoreService>();
        services.AddScoped<ICollanaService, CollanaService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<IReportService, ReportService>();
        return services;
    }
}
