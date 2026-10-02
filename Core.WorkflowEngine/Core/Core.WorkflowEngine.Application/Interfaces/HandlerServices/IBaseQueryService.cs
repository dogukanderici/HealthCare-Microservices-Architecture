using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessDefinitionDtos;
using Core.WorkflowEngine.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices
{
    public interface IBaseQueryService<T>
        where T : class, IEntity
    {
        public Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<T>? options = null);
        public Task<InternalServiceResponse<TResult>> GetDataByIdAsync<TResult>(Guid id);
        public Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasByFilterAsync<TResult>(DBQueryOptions<T> options);
        public Task<InternalServiceResponse<int>> GetDataCount(DBQueryOptions<T> options);
    }
}