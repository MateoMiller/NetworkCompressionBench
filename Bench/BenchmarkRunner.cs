using Bench;

public class BenchmarkRunner
{
    private readonly NetworkImitator networkImitator;
    private readonly byte[] donor;

    public BenchmarkRunner(NetworkImitator networkImitator)
    {
        this.networkImitator = networkImitator;
        this.donor = File.ReadAllBytes("book.txt");
    }

    public InterestingStatistic[] Run(int runsCount, int totalSize)
    {
        var results = new InterestingStatistic[runsCount];
        for (var i = 0; i < runsCount; i++)
        {
            var bytes = SubArray(donor, i * totalSize, totalSize);
            //random.NextBytes(bytes);
            results[i] = networkImitator.Immitate(bytes);
            if (!results[i].MakesSense)
                throw new Exception("WTF");
        }

        return results;
    }
    public static T[] SubArray<T>(T[] data, int index, int length)
    {
        T[] result = new T[length];
        if (index + length >= data.Length)
            throw new Exception($"Перебрал [{index}:{index + length}]");
        Array.Copy(data, index, result, 0, length);
        
        return result;
    }
}
