using System;

namespace App.Domain.Common;

public readonly struct Periodo
{
	public DateOnly DataDa { get; }
	public DateOnly DataA { get; }

	public Periodo(DateOnly dataDa, DateOnly dataA)
	{
		if (dataDa > dataA)
			throw new ArgumentException("DataDa non può essere maggiore di DataA");

		DataDa = dataDa;
		DataA = dataA;
	}

	public bool Contiene(DateOnly data) => data >= DataDa && data <= DataA;
}
