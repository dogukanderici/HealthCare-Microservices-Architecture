using HealthCare.Descriptions.Application.Common.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Interfaces
{
    public interface IDecryptionHelper
    {
        CryptionResponse<T> DecryptToken<T>(string encryptedToken);
        CryptionResponse<string> DecryptTokenForString(string encryptedToken);
    }
}