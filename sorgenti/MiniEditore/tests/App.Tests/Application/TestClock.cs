using App.Application.Abstractions;

namespace App.Tests.Application;

public sealed class TestClock : IClock
{
    public DateOnly Today => new(2026, 1, 15);
    public DateTime Now => new(2026, 1, 15, 10, 30, 0);
}
