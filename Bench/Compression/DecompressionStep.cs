namespace Bench.Compression;

public class DecompressionStep : ProcessingStepBase<byte[]>
{
    private readonly ICompressor compressor;

    public DecompressionStep(ICompressor compressor)
    {
        this.compressor = compressor;
    }

    protected override byte[] DoSomething(object[] parameters)
    {
        var compressedBytes = (byte[])parameters[0];
        return compressor.Decompress(compressedBytes);
    }
}