using System;

public abstract class ProcessorBase
{
	public BenchRessult<T> Process()
	{
		var sw = Stopwatch.StartNew();
		var elapsed = sw.Elapsed;

	}
}
