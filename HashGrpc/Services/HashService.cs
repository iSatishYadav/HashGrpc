using Grpc.Core;
using HashGrpc.Protos;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HashGrpc.Services
{
    public class HashService : Hash.HashBase
    {
        private readonly ILogger<HashService> _logger;

        public HashService(ILogger<HashService> logger)
        {
            _logger = logger;
        }
        public override Task<HashResponse> GetHash(HashRequest request, ServerCallContext context)
        {
            _logger.LogDebug("Getting hash for {request}", request);
            var hashResponse = new HashResponse
            {
                Hash = GetSha256Hash(request.Input)
            };
            _logger.LogDebug("Hash generated for {request} is {response}", request, hashResponse);
            return Task.FromResult(hashResponse);
        }

        private static string GetSha256Hash(string plainText)
        {
            // Create a SHA256 hash from string   
            using SHA256 sha256Hash = SHA256.Create();
            // Computing Hash - returns here byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));

            // now convert byte array to a string   
            StringBuilder stringbuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                stringbuilder.Append(bytes[i].ToString("x2"));
            }
            return stringbuilder.ToString();
        }
    }
}
