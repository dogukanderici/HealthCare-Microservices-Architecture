using Core.WorkflowEngine.Application.Commons.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces
{
    public interface IDecryptionHelper
    {
        CryptionResponse<T> DecryptToken<T>(string encryptedToken);
        CryptionResponse<string> DecryptTokenForString(string encryptedToken);
    }
}