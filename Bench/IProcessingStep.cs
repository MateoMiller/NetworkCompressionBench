namespace Bench;

public interface IProcessingStep<T>
{
    public StepResult<T> Process(params object[] parameters);
}