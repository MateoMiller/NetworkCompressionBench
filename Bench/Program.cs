using Autofac;
using Bench.Compression;
using Bench.Routing;

namespace Bench;

public static class Program
{
    private static IRouter SverdlovskRouter = new InMemoryRouter(
        TimeSpan.FromMilliseconds(1.71), TimeSpan.FromMilliseconds(0.000394));

    private static IRouter MoscowRouter = new InMemoryRouter(
        TimeSpan.FromMilliseconds(31.9605), TimeSpan.FromMilliseconds(0.0005628));

    private static IRouter FrankfurtRouter = new InMemoryRouter(
        TimeSpan.FromMilliseconds(69.2544), TimeSpan.FromMilliseconds(0.0003251));

    private static IRouter SydneyRouter = new InMemoryRouter(
        TimeSpan.FromMilliseconds(319.5805), TimeSpan.FromMilliseconds(0.0005118));

    private static ICompressor GzipCompressor = new GZipCompressor();
    private static ICompressor FakeCompressor = new NonCompressingCompressor();

    public static void Main()
    {
        var sizeMoreThanOnePacket = 100 * 1024;
        Run(nameof(SverdlovskRouter), GetContainer(SverdlovskRouter, GzipCompressor), sizeMoreThanOnePacket);
        Run(nameof(MoscowRouter), GetContainer(MoscowRouter, GzipCompressor), sizeMoreThanOnePacket);
        //  Run(nameof(FrankfurtRouter), GetContainer(FrankfurtRouter, GzipCompressor), sizeMoreThanOnePacket);
        //Run(nameof(SydneyRouter), GetContainer(SydneyRouter, GzipCompressor), sizeMoreThanOnePacket);
        Console.WriteLine("Теперь без сжатия");
        Run(nameof(SverdlovskRouter), GetContainer(SverdlovskRouter, FakeCompressor), sizeMoreThanOnePacket);
        Run(nameof(MoscowRouter), GetContainer(MoscowRouter, FakeCompressor), sizeMoreThanOnePacket);
        //Run(nameof(FrankfurtRouter), GetContainer(FrankfurtRouter, FakeCompressor), sizeMoreThanOnePacket);
        //Run(nameof(SydneyRouter), GetContainer(SydneyRouter, FakeCompressor), sizeMoreThanOnePacket);
        
        Console.WriteLine("\n\n");
        
        sizeMoreThanOnePacket = 150 * 1024;
        Run(nameof(SverdlovskRouter), GetContainer(SverdlovskRouter, GzipCompressor), sizeMoreThanOnePacket);
        Run(nameof(MoscowRouter), GetContainer(MoscowRouter, GzipCompressor), sizeMoreThanOnePacket);
        //  Run(nameof(FrankfurtRouter), GetContainer(FrankfurtRouter, GzipCompressor), sizeMoreThanOnePacket);
        //Run(nameof(SydneyRouter), GetContainer(SydneyRouter, GzipCompressor), sizeMoreThanOnePacket);
        Console.WriteLine("Теперь без сжатия");
        Run(nameof(SverdlovskRouter), GetContainer(SverdlovskRouter, FakeCompressor), sizeMoreThanOnePacket);
        Run(nameof(MoscowRouter), GetContainer(MoscowRouter, FakeCompressor), sizeMoreThanOnePacket);
        //Run(nameof(FrankfurtRouter), GetContainer(FrankfurtRouter, FakeCompressor), sizeMoreThanOnePacket);
        //Run(nameof(SydneyRouter), GetContainer(SydneyRouter, FakeCompressor), sizeMoreThanOnePacket);
        
        Console.WriteLine("\n\n");
        
        var onePacket = 1024;
        Run(nameof(SverdlovskRouter), GetContainer(SverdlovskRouter, GzipCompressor), onePacket);
        Run(nameof(MoscowRouter), GetContainer(MoscowRouter, GzipCompressor), onePacket);
//        Run(nameof(FrankfurtRouter), GetContainer(FrankfurtRouter, GzipCompressor), onePacket);
//        Run(nameof(SydneyRouter), GetContainer(SydneyRouter, GzipCompressor), onePacket);
        Console.WriteLine("Теперь без сжатия");
        Run(nameof(SverdlovskRouter), GetContainer(SverdlovskRouter, FakeCompressor), onePacket);
        Run(nameof(MoscowRouter), GetContainer(MoscowRouter, FakeCompressor), onePacket);
//        Run(nameof(FrankfurtRouter), GetContainer(FrankfurtRouter, FakeCompressor), onePacket);
//        Run(nameof(SydneyRouter), GetContainer(SydneyRouter, FakeCompressor), onePacket);

        Console.WriteLine("\n\n");

        onePacket = 300;
        Run(nameof(SverdlovskRouter), GetContainer(SverdlovskRouter, GzipCompressor), onePacket);
        Run(nameof(MoscowRouter), GetContainer(MoscowRouter, GzipCompressor), onePacket);
//        Run(nameof(FrankfurtRouter), GetContainer(FrankfurtRouter, GzipCompressor), onePacket);
//        Run(nameof(SydneyRouter), GetContainer(SydneyRouter, GzipCompressor), onePacket);
        Console.WriteLine("Теперь без сжатия");
        Run(nameof(SverdlovskRouter), GetContainer(SverdlovskRouter, FakeCompressor), onePacket);
        Run(nameof(MoscowRouter), GetContainer(MoscowRouter, FakeCompressor), onePacket);
//        Run(nameof(FrankfurtRouter), GetContainer(FrankfurtRouter, FakeCompressor), onePacket);
//        Run(nameof(SydneyRouter), GetContainer(SydneyRouter, FakeCompressor), onePacket);
    }

    public static void Run(string name, IContainer container, int totalSize)
    {
        var immitator = container.Resolve<BenchmarkRunner>();
        var stat = immitator.Run(10, totalSize);
        var avgSpent = stat.Average(x => x.TotalTime.TotalMilliseconds);
        Console.WriteLine($"{name}: avg:{avgSpent}," +
                          $"sent:{stat.Average(x => x.PacketsSent)}, compression:{stat.Average(x => x.CompressedPercentage)}," +
                          $"transferredSize={stat.Average(x => x.TransferredSize)}");
    }
    
    /*static void Main()
    {
        byte[] originalData = new byte[500_000];
        new Random(123).NextBytes(originalData);
        byte[] compressedData = GPTComp.Compress(originalData);
        byte[] decompressedData = GPTComp.Decompress(compressedData);

        Console.WriteLine("Original data: " + originalData.Length);
        Console.WriteLine("Compressed data: " + compressedData.Length);
        Console.WriteLine("Decompressed data: " + decompressedData.Length);
    }*/

    private static IContainer GetContainer(IRouter router, ICompressor compressor)
    {
        var builder = new ContainerBuilder();
        builder.Register(_ => compressor).AsSelf().AsImplementedInterfaces();
        builder.RegisterType<CompressionStep>().AsSelf().AsImplementedInterfaces();
        builder.RegisterType<DecompressionStep>().AsSelf().AsImplementedInterfaces();
        builder.Register(_ => router).AsSelf().AsImplementedInterfaces();
        builder.RegisterType<NetworkImitator>().AsSelf().AsImplementedInterfaces();
        builder.RegisterType<BenchmarkRunner>().AsSelf().AsImplementedInterfaces();
        return builder.Build();
    }
}