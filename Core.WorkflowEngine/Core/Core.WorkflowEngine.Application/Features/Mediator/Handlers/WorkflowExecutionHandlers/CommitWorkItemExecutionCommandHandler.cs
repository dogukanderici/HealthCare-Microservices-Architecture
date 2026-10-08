using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Features.BusinessRules.WorkItemExecutionPolicies;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkflowExecutionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.TaskTransitionServices;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkflowExecutionHandlers
{
    public class CommitWorkItemExecutionCommandHandler : IRequestHandler<CommitWorkItemExecutionCommand, InternalHandlerResponse<Guid>>
    {
        private readonly ITaskTransitionQueryService _taskTransitionQueryService;
        private readonly IWorkItemCommandService _workItemCommandService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IWorkItemExecutionUpdatePolicy _updatePolicy;

        public CommitWorkItemExecutionCommandHandler(ITaskTransitionQueryService taskTransitionQueryService, IWorkItemCommandService workItemCommandService, IMapper mapper, ICurrentUserService currentUserService, IWorkItemExecutionUpdatePolicy updatePolicy)
        {
            _taskTransitionQueryService = taskTransitionQueryService;
            _workItemCommandService = workItemCommandService;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _updatePolicy = updatePolicy;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CommitWorkItemExecutionCommand request, CancellationToken cancellationToken)
        {

            InternalPolicyResponse policyResponse = await _updatePolicy.ExecuteAllRuleAsync(request);

            if (!policyResponse.IsSuccess)
                return InternalHandlerResponse<Guid>.Failure(policyResponse.PolicyMessage);

            // 1. Form verileri json formatında db'ye kaydedilir.
            // 2. Aksiyon alınan workitem durumu Commit olacak şekilde güncellenir.
            // 3. Sonraki task için transition var mı kontrol edilir. Eğer varsa, yeni WorkItem oluşturulur ve task kullanıcısına atanır.
            // 4. Eğer yoksa, workflow instance tamamlanmış olur ve instance durumu Completed olarak güncellenir.
            // 5. Tüm db işlemleri tek bir transaction içinde yapılır. Eğer herhangi bir işlem başarısız olursa, tüm işlemler geri alınır.

            InternalServiceResponse<WorkItem> serviceResponse =
                await _workItemCommandService.GetDataForUpdateAsync(request.WorkItemId);

            if (serviceResponse.IsSuccess)
            {
                // Form verileri json formatında db'ye kaydedilir.
                // TO-DO

                // InitiatorWorkItem burada güncellenir.
                serviceResponse.Data.Status = 2; // Completed
                serviceResponse.Data.SelectedAction = request.ActionId;
                serviceResponse.Data.CompletedBy = _currentUserService.UserId;
                serviceResponse.Data.CompletedAt = _currentUserService.CurrentDate;

                await _workItemCommandService.UpdateAsync(serviceResponse.Data);

                // Sonraki task için transition var mı kontrol edilir.

                DBQueryOptions<ProcessTaskTransition> transitionOptions = new DBQueryOptions<ProcessTaskTransition>();
                transitionOptions.filter = x => (
                    (x.ProcessTaskId == request.ProcessTaskId) &&
                    (x.ActionId == request.ActionId)
                );

                InternalServiceResponse<IReadOnlyCollection<GetProcessTaskTransitionsByFilterQueryResult>> result =
                    await _taskTransitionQueryService.GetDatasByFilterAsync<GetProcessTaskTransitionsByFilterQueryResult>(transitionOptions);

                Guid.TryParse("00000000-0000-0000-0000-000000000000", out Guid newWorkItemId);

                foreach (var item in result.Data)
                {
                    // Transition varsa, yeni WorkItem oluşturulur.
                    WorkItem workItemFromDto = _mapper.Map<WorkItem>(request);

                    // Task kullanıcısına transition tablosundaki kayıtlı kullancıya atanır.
                    Guid assignedUserId = item.ProcessTask.AssignedUser;

                    workItemFromDto.AssignedUserId = assignedUserId;
                    workItemFromDto.InstanceId = request.InstanceId;
                    workItemFromDto.StepId = item.NextTaskId;

                    InternalServiceResponse<Guid> workItemResult = await _workItemCommandService.CreateAsync(workItemFromDto, cancellationToken);
                    newWorkItemId = workItemResult.Data;

                    return InternalHandlerResponse<Guid>.Success(newWorkItemId);
                }

                return InternalHandlerResponse<Guid>.Success(newWorkItemId);
            }
            else
            {
                return InternalHandlerResponse<Guid>.Failure(InternalHandlerConstants.WorkItemNotFound);
            }
        }
    }
}