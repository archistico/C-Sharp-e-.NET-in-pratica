using System;

namespace App.Application.Abstractions;

public interface IClock
{
    DateOnly Today { get; }
    DateTime Now { get; }
}
