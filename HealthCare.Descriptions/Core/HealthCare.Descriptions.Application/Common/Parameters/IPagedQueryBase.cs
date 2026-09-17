using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Common.Parameters
{
    public interface IPagedQueryBase
    {
        public string? Token { get; set; }
    }
}