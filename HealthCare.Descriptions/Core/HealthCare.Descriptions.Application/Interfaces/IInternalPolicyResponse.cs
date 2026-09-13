using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Interfaces
{
    public interface IInternalPolicyResponse
    {
        bool IsSuccess { get; set; }
        string? BusinessRuleError { get; set; }
        List<string>? BusinessRuleErrors { get; set; }
    }
}
