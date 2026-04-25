using App.Application.Abstractions;

namespace App.Infrastructure.Services;

internal class SystemClock : IClock
{
    public DateOnly Today => DateOnly.FromDateTime(DateTime.Today);
    public DateTime Now => DateTime.Now;
}
