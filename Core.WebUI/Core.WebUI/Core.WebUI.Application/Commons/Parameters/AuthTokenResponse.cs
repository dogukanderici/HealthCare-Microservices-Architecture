using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Commons.Parameters
{
    public class AuthTokenResponse
    {
        public bool Status { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpireIn { get; set; }
        public List<ClaimDto> Claims { get; set; }
    }

    public class ClaimDto
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}