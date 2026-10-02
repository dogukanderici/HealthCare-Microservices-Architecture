using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.InboxQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InboxResults;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InboxHandlers
{
    public class GetInboxByUserIdQueryHandler : IRequestHandler<GetInboxByUserIdQuery, InternalHandlerResponse<IReadOnlyCollection<GetInboxByUserIdQueryResult>>>
    {
        private readonly IWorkItemQueryService _workItemQueryService;
        private readonly IMapper _mapper;

        public GetInboxByUserIdQueryHandler(IWorkItemQueryService workItemQueryService, IMapper mapper)
        {
            _workItemQueryService = workItemQueryService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetInboxByUserIdQueryResult>>> Handle(GetInboxByUserIdQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();
            dBQueryOptions.filter = x => (
                (x.AssignedUserId == request.AssignedUserId) &&
                (x.Status == 1)
            );
            dBQueryOptions.thenIncludes = new Dictionary<Expression<Func<WorkItem, object>>, List<Expression<Func<object, object>>>>()
            {
                {
                        x => x.Instance,
                        new List<Expression<Func<object, object>>>
                        {
                            p=>((Instance)p).ProcessDefinition
                        }
                },
                {
                        x => x.ProcessTask,
                        new List<Expression<Func<object, object>>>()
                }
            };

            InternalServiceResponse<IReadOnlyCollection<GetWorkItemsByFilterQueryResult>> serviceResponse =
                await _workItemQueryService.GetWorkItemByFilterAsync<GetWorkItemsByFilterQueryResult>(dBQueryOptions);

            if (!serviceResponse.IsSuccess)
                return InternalHandlerResponse<IReadOnlyCollection<GetInboxByUserIdQueryResult>>.Failure(serviceResponse.ServiceMessage);

            IReadOnlyCollection<GetInboxByUserIdQueryResult> mappedData = _mapper.Map<IReadOnlyCollection<GetInboxByUserIdQueryResult>>(serviceResponse.Data);

            return InternalHandlerResponse<IReadOnlyCollection<GetInboxByUserIdQueryResult>>.Success(mappedData);
        }
    }
}