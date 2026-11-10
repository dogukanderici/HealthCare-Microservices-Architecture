using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Commons.Parameters
{
    public class ApiSetting
    {
        public string IdentityServerBaseUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string HttpClientName { get; set; }
    }
}
