using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Persistence.ExternalServices
{
    public abstract class BaseService
    {
        protected abstract string ServiceBaseUrl { get; set; }
        protected abstract string HttpClientName { get; set; }
    }
}