using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Commons.Utilities
{
    public class HashUtility
    {
        public static string GenerateHashKey(string baseKey)
        {
            return ExecuteSHA256(baseKey);
        }

        public static string GenerateHashKey(IReadOnlyCollection<string> baseKeyList)
        {
            string rawKey = string.Join(":", baseKeyList);

            return ExecuteSHA256(rawKey);
        }

        private static string ExecuteSHA256(string rawKey)
        {
            byte[] stringData = Encoding.UTF8.GetBytes(rawKey);

            byte[] hashData = SHA256.HashData(stringData);


            return Convert.ToHexString(hashData).ToLower();
        }
    }
}