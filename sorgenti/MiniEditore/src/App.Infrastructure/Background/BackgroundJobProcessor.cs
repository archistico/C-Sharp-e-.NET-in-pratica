using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Reflection;
using System;

namespace App.Infrastructure.Background;

internal class BackgroundJobProcessor : BackgroundService
{
    private readonly IBackgroundTaskQueue _queue;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<BackgroundJobProcessor> _logger;

    public BackgroundJobProcessor(IBackgroundTaskQueue queue, IServiceScopeFactory scopeFactory, ILogger<BackgroundJobProcessor> logger)
    {
        _queue = queue;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var job in _queue.DequeueAllAsync(stoppingToken))
        {
            try
            {
                await ProcessJobAsync(job, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing background job {JobType}", job?.GetType().Name);
            }
        }
    }

    private async Task ProcessJobAsync(BackgroundJob job, CancellationToken cancellationToken)
    {
        switch (job)
        {
            case GenerateSalesReportJob gen:
                using (var scope = _scopeFactory.CreateScope())
                {
                    var sp = scope.ServiceProvider;
                    var reportType = Type.GetType("App.Application.Report.IReportService, App.Application");
                    if (reportType != null)
                    {
                        var svc = sp.GetService(reportType);
                        if (svc != null)
                        {
                            var method = reportType.GetMethod("GetVenditePerLibroAsync");
                            if (method != null)
                            {
                                var task = (Task)method.Invoke(svc, new object[] { gen.From, gen.To, cancellationToken })!;
                                await task;
                                _logger.LogInformation("Generated sales report (reflection)");
                            }
                        }
                    }
                }
                break;

            case RecalculateDashboardJob:
                using (var scope = _scopeFactory.CreateScope())
                {
                    var sp = scope.ServiceProvider;
                    var dashType = Type.GetType("App.Application.Dashboard.IDashboardService, App.Application");
                    if (dashType != null)
                    {
                        var svc = sp.GetService(dashType);
                        if (svc != null)
                        {
                            var method = dashType.GetMethod("GetSummaryAsync");
                            if (method != null)
                            {
                                var task = (Task)method.Invoke(svc, new object[] { cancellationToken })!;
                                await task;
                                _logger.LogInformation("Recalculated dashboard (reflection)");
                            }
                        }
                    }
                }
                break;

            default:
                _logger.LogWarning("Unknown background job type: {JobType}", job?.GetType().Name);
                break;
        }
    }
}
