using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.WorkItemBusinessRules;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Application.ServiceDtos.WorkItemServiceDtos;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Services
{
    public class WorkItemService : IWorkItemService
    {
        private readonly IRepository<WorkItem> _repository;
        private readonly ILogger<WorkItemService> _logger;
        private readonly IWorkItemBusinessRule _businessRule;

        public WorkItemService(IRepository<WorkItem> repository, ILogger<WorkItemService> logger, IWorkItemBusinessRule businessRule)
        {
            _repository = repository;
            _logger = logger;
            _businessRule = businessRule;
        }

        public async Task<InternalServiceResponse<WorkItem>> GetWorkItemByIdAsync(Guid id)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();

            Expression<Func<WorkItem, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            WorkItem result = await _repository.GetDataAsync(dBQueryOptions);

            return InternalServiceResponse<WorkItem>.Success(result);
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<WorkItem>>> GetWorkItemByFilterAsync(WorkItemFilterDto workItemFilterDto)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();

            Expression<Func<WorkItem, bool>> filter = x => (
                (!workItemFilterDto.InstanceId.HasValue || x.InstanceId == workItemFilterDto.InstanceId) &&
                (!workItemFilterDto.WorkItemId.HasValue || x.Id == workItemFilterDto.WorkItemId) &&
                (!workItemFilterDto.AssignedUserId.HasValue || x.AssignedUserId == workItemFilterDto.AssignedUserId) &&
                (!workItemFilterDto.Status.HasValue || x.Status == workItemFilterDto.Status) &&
                (!workItemFilterDto.CreatedAt.HasValue || x.CreatedAt == workItemFilterDto.CreatedAt)
            );
            dBQueryOptions.filter = filter;

            IReadOnlyCollection<WorkItem> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalServiceResponse<IReadOnlyCollection<WorkItem>>.Success(result);
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<WorkItem>>> GetWorkItemByFilterAsync(DBQueryOptions<WorkItem> dBQueryOptions)
        {
            IReadOnlyCollection<WorkItem> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalServiceResponse<IReadOnlyCollection<WorkItem>>.Success(result);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(WorkItem entity, CancellationToken cancellationToken)
        {
            Guid id = await _repository.CreateDataAsync(entity);

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(WorkItemService),
                    LogConstants.SuccessMessages.DataCreatedSuccessfully);

            return InternalServiceResponse<Guid>.Success(id);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(WorkItem entity, CancellationToken cancellationToken)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();

            Expression<Func<WorkItem, bool>> filter = x => x.Id == entity.Id;
            dBQueryOptions.filter = filter;

            // Veri yoksa true döner.
            bool checkAllRules = await _businessRule.CheckAllRulesAsync(dBQueryOptions);

            if (checkAllRules)
            {
                _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(UpdateWorkItemCommandHandler),
                        LogConstants.ErrorMessages.DataUpdateFailed);

                return InternalServiceResponse<DateTimeOffset>.Failure();
            }

            DateTimeOffset updatedDate = await _repository.UpdateDataAsync(entity);

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                nameof(UpdateWorkItemCommandHandler),
                LogConstants.SuccessMessages.DataUpdatedSuccessfully);

            return InternalServiceResponse<DateTimeOffset>.Success(updatedDate);
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();

            Expression<Func<WorkItem, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            WorkItem result = await _repository.GetDataAsync(dBQueryOptions);

            if (result != null)
            {
                await _repository.DeleteDataAsync(result);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(DeleteWorkItemCommandHandler),
                    LogConstants.SuccessMessages.DataDeletedSuccessfully);

                return InternalServiceResponse<bool>.Success(true);
            }

            _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(DeleteWorkItemCommandHandler),
                    LogConstants.ErrorMessages.DataDeletionFailed);

            return InternalServiceResponse<bool>.Failure();
        }
    }
}