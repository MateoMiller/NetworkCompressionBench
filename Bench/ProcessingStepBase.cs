using System.Diagnostics;

namespace Bench;

public abstract class ProcessingStepBase<T>
{
	public StepResult<T> Process(params object[] parameters)
	{
		var sw = Stopwatch.StartNew();
		var result = DoSomething(parameters);
		var elapsed = sw.Elapsed;
		return new StepResult<T>(elapsed, result);
	}

	protected abstract T DoSomething(params object[] parameters);
}