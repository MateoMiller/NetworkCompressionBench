using System.IO.Compression;

namespace Bench.Compression;

public class GZipCompressor : ICompressor
{
    public byte[] Compress(byte[] data)
    {
        using var output = new MemoryStream();
        using (var source = new MemoryStream(data))
        {
            using (var compressor = new GZipStream(output, CompressionLevel.SmallestSize))
            {
                source.CopyTo(compressor);
            }
        }

        var outputBytes = output.ToArray();
        return outputBytes;
    }

    public byte[] Decompress(byte[] compressedData)
    {
        using var output = new MemoryStream();
        using (var source = new MemoryStream(compressedData))
        {
            using (var decompressor = new GZipStream(source, CompressionMode.Decompress))
            {
                decompressor.CopyTo(output);
            }
        }

        var outputBytes = output.ToArray();
        return outputBytes;
    }
}