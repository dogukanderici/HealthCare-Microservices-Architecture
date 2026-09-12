using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Interfaces
{
    public interface IValidationResult
    {
        public List<string>? ValidationErrors { get; set; }

        static abstract IValidationResult WithValidationErrors(List<string> errors);
    }
}
