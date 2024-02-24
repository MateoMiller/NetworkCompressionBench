using ICSharpCode.SharpZipLib.GZip;

public interface ICompressor
{
    public byte[] Compress(byte[] rawData);
    public byte[] Decompress(byte[] compressedData);
}

public class GZipCompressor : ICompressor
{
    //TODO Подумать про кэширование буффера

    public byte[] Compress(byte[] data)
    {
        var memstream = new MemoryStream(data);
        var output = new MemoryStream();
        GZip.Compress(memstream, output, true);
        return output.ToArray();
    }

    public byte[] Decompress(byte[] compressedData)
    {
        var memstream = new MemoryStream(compressedData);
        var output = new MemoryStream();
        GZip.Decompress(memstream, output, true);
        return output.ToArray();
    }
}