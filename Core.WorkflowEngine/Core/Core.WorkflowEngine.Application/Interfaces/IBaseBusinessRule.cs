using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces
{
    public interface IBaseBusinessRule<TEntity, TQueryData>
    {
        Task<int> ExistingDataControlAsync(TQueryData queryData);
    }
}
