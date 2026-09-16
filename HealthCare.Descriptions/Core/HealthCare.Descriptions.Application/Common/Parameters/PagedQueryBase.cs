using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Common.Parameters
{
    public class PagedQueryBase
    {
        public string? Token { get; set; }
        public int TakeCount { get; set; } = 10;
    }
}