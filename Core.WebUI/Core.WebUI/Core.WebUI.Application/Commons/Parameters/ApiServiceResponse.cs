using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Commons.Parameters
{
    public class ApiServiceResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public T Data { get; set; }

        public string PagingToken { get; set; }
        public bool IsLastPage { get; set; }

        public List<string>? ValidationErrors { get; set; }
        public List<string>? BusinessRuleErrors { get; set; }
    }
}