using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces
{
    public interface IValidationResult
    {
        List<string>? ValidationErrors { get; set; }
    }
}
