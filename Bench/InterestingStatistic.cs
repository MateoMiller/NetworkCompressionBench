namespace Bench;

public record InterestingStatistic
{
    public int PacketsSent;
    public int InitialSize;
    public int TransferredSize;
    public int DecompressedSize;

    public TimeSpan SpentOnCompression;
    public TimeSpan SpentOnRouting;
    public TimeSpan SpentOnDecompression;
    public TimeSpan TotalTime => SpentOnCompression + SpentOnRouting + SpentOnDecompression;

    public double CompressedPercentage => (double)TransferredSize / InitialSize;
    public bool MakesSense => InitialSize == DecompressedSize;
}