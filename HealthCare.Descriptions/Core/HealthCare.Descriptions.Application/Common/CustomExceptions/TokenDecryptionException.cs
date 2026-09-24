using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Common.CustomExceptions
{
    public class TokenDecryptionException : Exception
    {
        public TokenDecryptionException(string message) : base(message)
        {

        }
    }
}