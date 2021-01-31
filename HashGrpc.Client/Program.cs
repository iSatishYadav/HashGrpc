using Grpc.Net.Client;
using HashGrpc.Protos;
using System;
using System.Threading.Tasks;
using static HashGrpc.Protos.Hash;

namespace HashGrpc.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new HashClient(channel);
            var response = await client.GetHashAsync(new HashRequest { Input = "Test" });
            Console.WriteLine($"Hash: {response.Hash}");
            Console.ReadKey();
        }
    }
}
