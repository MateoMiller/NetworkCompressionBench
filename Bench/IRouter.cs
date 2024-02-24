using System;

public interface IRouter
{
    public void PerformTransition(byte[] bytes);
}

public class InMemoryRouter : IRouter
{
    private TimeSpan defaultSleepTime = new TimeSpan();
    private TimeSpan sleepTimeForOneByte = new TimeSpan();
    public void PerformTransition(byte[] bytes)
    {
        Thread.Sleep(defaultSleepTime);
        Thread.Sleep(bytes.Length * sleepTimeForOneByte);
    }
}