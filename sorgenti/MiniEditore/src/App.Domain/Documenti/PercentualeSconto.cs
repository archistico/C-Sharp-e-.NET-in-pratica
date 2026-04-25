using System;

namespace App.Domain.Documenti;

public readonly struct PercentualeSconto
{
    public decimal Value { get; }

    public PercentualeSconto(decimal value)
    {
        if (value < 0 || value > 100)
            throw new ArgumentOutOfRangeException(nameof(value), "Sconto deve essere tra 0 e 100");

        Value = value;
    }

    public decimal FattoreMoltiplicativo => 1m - (Value / 100m);
}
