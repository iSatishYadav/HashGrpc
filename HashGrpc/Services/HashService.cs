using Grpc.Core;
using HashGrpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HashGrpc.Services
{
    public class HashService : Hash.HashBase
    {
        public override Task<HashResponse> GetHash(HashRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HashResponse
            {
                Hash = GetSha256Hash(request.Input)
            });
        }

        private static string GetSha256Hash(string plainText)
        {
            // Create a SHA256 hash from string   
            using (SHA256 sha256Hash = SHA256.Create())
            {
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
}
