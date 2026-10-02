using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskBusinessRules;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Services.ProcessTaskServices
{
    public class ProcessTaskCommandService : IProcessTaskCommandService
    {
        private readonly IRepository<ProcessTask> _repository;

        public ProcessTaskCommandService(IRepository<ProcessTask> repository)
        {
            _repository = repository;
        }

        public async Task<InternalServiceResponse<ProcessTask>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<ProcessTask> dBQueryOptions = new DBQueryOptions<ProcessTask>();
            dBQueryOptions.filter = x => x.Id == id;

            ProcessTask repoResponse = await _repository.GetDataAsync(dBQueryOptions);

            if (repoResponse == null)
                return InternalServiceResponse<ProcessTask>.Failure("Data not found.");

            return InternalServiceResponse<ProcessTask>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(ProcessTask entity, CancellationToken cancellationToken)
        {
            Guid repoResponse = await _repository.CreateDataAsync(entity);

            return InternalServiceResponse<Guid>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(ProcessTask entity)
        {
            DateTimeOffset repoResponse = await _repository.UpdateDataAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id)
        {
            InternalServiceResponse<ProcessTask> existedData = await GetDataForUpdateAsync(id);

            if (!existedData.IsSuccess)
                return InternalServiceResponse<bool>.Failure("Data not found.");

            await _repository.DeleteDataAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}