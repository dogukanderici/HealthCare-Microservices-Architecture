using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService;
using Core.WorkflowEngine.Domain.Entities;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Services.ProcessTaskServices
{
    public class ProcessTaskQueryService : IProcessTaskQueryService
    {
        private readonly IRepository<ProcessTask> _repository;

        public ProcessTaskQueryService(IRepository<ProcessTask> repository)
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
    }
}
