using Core.WorkflowEngine.Application.Features.Commons.Utilities;
using Core.WorkflowEngine.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Wrappers
{
    public class CacheKeyGenerator
    {
        public static string GenerateCacheKey(IReadOnlyCollection<string> baseKeyList)
        {
            return HashUtility.GenerateHashKey(baseKeyList);
        }

        public static string GenerateCacheKey(string baseKey)
        {
            return HashUtility.GenerateHashKey(baseKey);
        }
    }
}