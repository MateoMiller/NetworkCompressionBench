namespace Bench.Compression;

public class NonCompressingCompressor : ICompressor
{
    public byte[] Compress(byte[] rawData)
    {
        return rawData;
    }

    public byte[] Decompress(byte[] compressedData)
    {
        return compressedData;
    }
}