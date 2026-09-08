using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessDefinitionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices
{
    public interface IBaseQueryService<T, TFilterDto>
        where T : class
        where TFilterDto : class
    {
        public Task<InternalServiceResponse<IReadOnlyCollection<T>>> GetDatasAsync(TFilterDto? filterDto = null);
        public Task<InternalServiceResponse<T>> GetDataByIdAsync(Guid id);
        public Task<InternalServiceResponse<IReadOnlyCollection<T>>> GetDatasByFilterAsync(TFilterDto filterDto);
        public Task<InternalServiceResponse<int>> GetDataCount(TFilterDto filterDto);
    }
}