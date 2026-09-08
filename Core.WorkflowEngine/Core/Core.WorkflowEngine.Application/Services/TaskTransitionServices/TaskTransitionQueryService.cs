using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.TaskTransitionServices;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessTaskTransitionDtos;
using Core.WorkflowEngine.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Services.TaskTransitionServices
{
    public class TaskTransitionQueryService : ITaskTransitionQueryService
    {
        private readonly IRepository<ProcessTaskTransition> _repository;
        private readonly ILogger<TaskTransitionQueryService> _logger;

        public TaskTransitionQueryService(IRepository<ProcessTaskTransition> repository, ILogger<TaskTransitionQueryService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<ProcessTaskTransition>>> GetDatasAsync(TaskTransitionFilterDto? filterDto = null)
        {
            DBQueryOptions<ProcessTaskTransition> dBQueryOptions = new DBQueryOptions<ProcessTaskTransition>();

            if (filterDto != null)
            {
                dBQueryOptions = GetDbQueryOptions(filterDto);
            }

            IReadOnlyCollection<ProcessTaskTransition> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalServiceResponse<IReadOnlyCollection<ProcessTaskTransition>>.Success(result);
        }

        public async Task<InternalServiceResponse<int>> GetDataCount(TaskTransitionFilterDto filterDto)
        {
            DBQueryOptions<ProcessTaskTransition> dBQueryOptions = GetDbQueryOptions(filterDto, false);

            int result = await _repository.GetAllDataCountAsync(dBQueryOptions);

            return InternalServiceResponse<int>.Success(result);
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<ProcessTaskTransition>>> GetDatasByFilterAsync(TaskTransitionFilterDto filterDto)
        {
            DBQueryOptions<ProcessTaskTransition> dBQueryOptions = GetDbQueryOptions(filterDto);

            IReadOnlyCollection<ProcessTaskTransition> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalServiceResponse<IReadOnlyCollection<ProcessTaskTransition>>.Success(result);
        }

        public async Task<InternalServiceResponse<ProcessTaskTransition>> GetDataByIdAsync(Guid id)
        {
            DBQueryOptions<ProcessTaskTransition> dBQueryOptions = new DBQueryOptions<ProcessTaskTransition>();

            Expression<Func<ProcessTaskTransition, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            ProcessTaskTransition result = await _repository.GetDataAsync(dBQueryOptions);

            return InternalServiceResponse<ProcessTaskTransition>.Success(result);
        }

        private DBQueryOptions<ProcessTaskTransition> GetDbQueryOptions(TaskTransitionFilterDto filterDto, bool isRelationalQuery = true)
        {
            DBQueryOptions<ProcessTaskTransition> dBQueryOptions = new DBQueryOptions<ProcessTaskTransition>();

            Expression<Func<ProcessTaskTransition, bool>> filter = x => (
            (!filterDto.ProcessTaskId.HasValue || x.ProcessTaskId == filterDto.ProcessTaskId) &&
            (!filterDto.ActionId.HasValue || x.ActionId == filterDto.ActionId) &&
            (!filterDto.IsActive.HasValue || x.IsActive == filterDto.IsActive)
            );

            dBQueryOptions.filter = filter;

            if (isRelationalQuery)
            {
                List<Expression<Func<ProcessTaskTransition, object>>> include = [
                    x => x.ProcessTask,
                    x => x.NextTask
                    ];

                dBQueryOptions.includes = include;
            }

            return dBQueryOptions;
        }
    }
}