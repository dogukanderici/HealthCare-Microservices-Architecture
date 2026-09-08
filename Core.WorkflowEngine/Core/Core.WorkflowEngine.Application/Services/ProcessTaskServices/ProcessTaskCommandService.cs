using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
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

        public async Task<ProcessTask> GetWorkItemForUpdateAsync(Guid id)
        {
            DBQueryOptions<ProcessTask> dBQueryOptions = new DBQueryOptions<ProcessTask>();

            Expression<Func<ProcessTask, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            ProcessTask result = await _repository.GetDataAsync(dBQueryOptions);

            return result;
        }

        public Task<InternalServiceResponse<Guid>> CreateAsync(ProcessTask entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(ProcessTask entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<InternalServiceResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
