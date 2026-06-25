using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces
{
    public interface IInternalCommandResponse
    {
        bool IsSuccess { get; set; }
        string InternalMessage { get; set; }
    }
}