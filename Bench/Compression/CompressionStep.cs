namespace Bench.Compression;

public class CompressionStep : ProcessingStepBase<byte[]>
{
    private readonly ICompressor compressor;

    public CompressionStep(ICompressor compressor)
    {
        this.compressor = compressor;
    }

    protected override byte[] DoSomething(object[] parameters)
    {
        var rawBytes = (byte[])parameters[0];
        return compressor.Compress(rawBytes);
    }
}