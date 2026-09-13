using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Interfaces
{
    public interface IBusinessRuleResponse
    {
        List<string>? BusinessRuleErrors { get; set; }

        static abstract IBusinessRuleResponse ListBusinessRuleErrors(List<string> errorList);
    }
}