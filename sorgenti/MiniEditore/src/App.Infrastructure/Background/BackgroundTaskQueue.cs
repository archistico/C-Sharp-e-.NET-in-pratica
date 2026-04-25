using System.Threading.Channels;

namespace App.Infrastructure.Background;

public interface IBackgroundTaskQueue
{
    ValueTask QueueAsync(BackgroundJob job, CancellationToken cancellationToken = default);
    IAsyncEnumerable<BackgroundJob> DequeueAllAsync(CancellationToken cancellationToken = default);
}

internal class BackgroundTaskQueue : IBackgroundTaskQueue
{
    private readonly Channel<BackgroundJob> _channel;

    public BackgroundTaskQueue()
    {
        var options = new BoundedChannelOptions(100)
        {
            FullMode = BoundedChannelFullMode.Wait
        };
        _channel = Channel.CreateBounded<BackgroundJob>(options);
    }

    public async ValueTask QueueAsync(BackgroundJob job, CancellationToken cancellationToken = default)
    {
        await _channel.Writer.WriteAsync(job, cancellationToken);
    }

    public async IAsyncEnumerable<BackgroundJob> DequeueAllAsync([System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in _channel.Reader.ReadAllAsync(cancellationToken))
        {
            yield return item;
        }
    }
}

public abstract record BackgroundJob;

public sealed record GenerateSalesReportJob(DateOnly From, DateOnly To) : BackgroundJob;
public sealed record RecalculateDashboardJob() : BackgroundJob;
