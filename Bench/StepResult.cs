namespace Bench;

public record StepResult<T>(TimeSpan Elapsed, T Result);