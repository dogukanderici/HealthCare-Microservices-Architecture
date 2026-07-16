using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces
{
    public interface IDynamicPropertyJoiner
    {
        string JoinValues<T>(T obj, string seperator = ":")
            where T : class;
    }
}