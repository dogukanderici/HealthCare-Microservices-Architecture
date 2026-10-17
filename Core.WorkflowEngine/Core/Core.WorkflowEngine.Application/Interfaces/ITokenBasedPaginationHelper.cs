using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Core.WorkflowEngine.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces
{
    public interface ITokenBasedPaginationHelper<T, TPropType, TDto>
        where T : class, IEntity
    {
        Task<InternalHandlerResponse<IReadOnlyCollection<TDto>>> PaginationAsync(PaginationConfig<T, TPropType, TDto> config, DBQueryOptions<T>? options = null);
    }
}
