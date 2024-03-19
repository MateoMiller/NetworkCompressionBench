namespace Bench.Compression;

public interface ICompressor
{
    public byte[] Compress(byte[] rawData);
    public byte[] Decompress(byte[] compressedData);
}