using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Interfaces
{
    public interface IInternalServiceResponse
    {
        bool IsSuccess { get; set; }
        string ServiceMessage { get; set; }
    }
}
