using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Services
{
    public class ProcessTaskService : IProcessTaskService
    {
        private readonly IRepository<ProcessTask> _repository;

        public ProcessTaskService(IRepository<ProcessTask> repository)
        {
            _repository = repository;
        }

        public Task<ProcessTask> GetDataByProcessIdAsync(Guid processId)
        {
            DBQueryOptions<ProcessTask> dBQueryOptions = new DBQueryOptions<ProcessTask>();

            Expression<Func<ProcessTask, bool>> filter = x => (x.ProcessId == processId && x.IsStartStep == true);
            dBQueryOptions.filter = filter;

            return _repository.GetDataAsync(dBQueryOptions);
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
