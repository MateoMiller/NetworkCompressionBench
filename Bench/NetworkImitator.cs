using Bench.Compression;
using MoreLinq;
using Bench.Routing;

namespace Bench;

public class NetworkImitator
{
    private readonly CompressionStep compressor;
    private readonly DecompressionStep decompressor;
    private readonly IRouter router;

    public NetworkImitator(IRouter router, DecompressionStep decompressor, CompressionStep compressor)
    {
        this.router = router;
        this.decompressor = decompressor;
        this.compressor = compressor;
    }

    public InterestingStatistic Immitate(byte[] bytes)
    {
        var compressedResult = compressor.Process(bytes);
        var packets = BatchIntoPackets(compressedResult.Result);
        var routed = new List<byte[]>();
        var routedTime = new TimeSpan();
        foreach (var packet in packets)
        {
            var routedResult = router.Process(packet);
            routed.Add(routedResult.Result);
            routedTime += routedResult.Elapsed;
        }

        var routedSummary = routed.SelectMany(x => x).ToArray();

        var decompressedResult = decompressor.Process(routedSummary);
        return new InterestingStatistic
        {
            PacketsSent = packets.Length,
            InitialSize = bytes.Length,
            TransferredSize = routed.Sum(x => x.Length),
            DecompressedSize = decompressedResult.Result.Length,
            SpentOnCompression = compressedResult.Elapsed,
            SpentOnRouting = routedTime,
            SpentOnDecompression = decompressedResult.Elapsed
        };
    }

    private static byte[][] BatchIntoPackets(byte[] initialBytes)
    {
        const int mtu = 1500;
        return initialBytes.Batch(mtu).ToArray();
    }
}