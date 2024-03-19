namespace Bench.Routing;

public class InMemoryRouter : IRouter
{
    private TimeSpan defaultSleepTime;
    private TimeSpan sleepTimeForOneByte;

    public InMemoryRouter(TimeSpan defaultSleepTime, TimeSpan sleepTimeForOneByte)
    {
        this.defaultSleepTime = defaultSleepTime;
        this.sleepTimeForOneByte = sleepTimeForOneByte;
    }

    public StepResult<byte[]> Process(params object[] parameters)
    {
        var bytes = (byte[]) parameters[0];
        var elapsedTime = defaultSleepTime + bytes.Length * sleepTimeForOneByte;
        return new StepResult<byte[]>(elapsedTime, bytes);
    }
}