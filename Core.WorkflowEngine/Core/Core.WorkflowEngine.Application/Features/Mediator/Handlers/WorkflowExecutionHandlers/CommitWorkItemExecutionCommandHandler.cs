using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkflowExecutionCommands;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessTaskTransitionDtos;
using Core.WorkflowEngine.Application.ServiceDtos.WorkItemServiceDtos;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkflowExecutionHandlers
{
    public class CommitWorkItemExecutionCommandHandler : IRequestHandler<CommitWorkItemExecutionCommand, InternalHandlerResponse<Guid>>
    {
        private readonly ITaskTransitionService _taskTransitionService;
        private readonly IWorkItemService _workItemService;
        private readonly IMapper _mapper;

        public CommitWorkItemExecutionCommandHandler(ITaskTransitionService taskTransitionService, IWorkItemService workItemService, IMapper mapper)
        {
            _taskTransitionService = taskTransitionService;
            _workItemService = workItemService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CommitWorkItemExecutionCommand request, CancellationToken cancellationToken)
        {
            // 1. Form verileri json formatında db'ye kaydedilir.
            // 2. Aksiyon alınan workitem durumu Commit olacak şekilde güncellenir.
            // 3. Sonraki task için transition var mı kontrol edilir. Eğer varsa, yeni WorkItem oluşturulur ve task kullanıcısına atanır.
            // 4. Eğer yoksa, workflow instance tamamlanmış olur ve instance durumu Completed olarak güncellenir.
            // 5. Tüm db işlemleri tek bir transaction içinde yapılır. Eğer herhangi bir işlem başarısız olursa, tüm işlemler geri alınır.

            WorkItemFilterDto dataFromDto = _mapper.Map<WorkItemFilterDto>(request);
            InternalServiceResponse<WorkItem> workItem = await _workItemService.GetWorkItemByIdAsync(dataFromDto);

            if (workItem != null)
            {
                // Form verileri json formatında db'ye kaydedilir.
                // TO-DO

                // InitiatorWorkItem burada güncellenir.
                workItem.Data.Status = 2; // Completed
                await _workItemService.UpdateAsync(workItem.Data, cancellationToken);

                // Sonraki task için transition var mı kontrol edilir.
                TaskTransitionFilterDto filterFromDto = _mapper.Map<TaskTransitionFilterDto>(request);
                InternalServiceResponse<List<ProcessTaskTransition>> result = await _taskTransitionService.GetDatasByFilterAsync(filterFromDto);

                Guid.TryParse("00000000-0000-0000-0000-000000000000", out Guid newWorkItemId);

                foreach (var item in result.Data)
                {
                    // Transition varsa, yeni WorkItem oluşturulur.
                    WorkItem workItemFromDto = _mapper.Map<WorkItem>(request);

                    // Task kullanıcısına transition tablosundaki kayıtlı kullancıya atanır.
                    // TO-DO

                    InternalServiceResponse<Guid> workItemResult = await _workItemService.CreateAsync(workItemFromDto, cancellationToken);
                    newWorkItemId = workItemResult.Data;

                    return InternalHandlerResponse<Guid>.Success(newWorkItemId);
                }

                return InternalHandlerResponse<Guid>.Success(newWorkItemId);
            }
            else
            {
                return InternalHandlerResponse<Guid>.Failure(InternalCommandConstants.WorkItemNotFound);
            }
        }
    }
}