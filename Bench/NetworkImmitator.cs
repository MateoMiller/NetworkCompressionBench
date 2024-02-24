using System;
using System.Diagnostics;

public class NetworlImmitator
{
    private readonly ICompressor compressor;
    private readonly IRouter router;

    public NetworlImmitator(ICompressor compressor, IRouter router)
	{
        this.compressor = compressor;
        this.router = router;
    }

    public BenchResult Immitate(byte[] bytes)
    {
        var sw = Stopwatch.StartNew();
        var compressed = compressor.Compress(bytes);
        router.PerformTransition(compressed);
        var decompressed = compressor.Decompress(bytes);
        return new BenchResult((double)compressed.Length / bytes.Length, sw.Elapsed);
    }
}

public record BenchResult(double CompressionPercentage, TimeSpan Time);
