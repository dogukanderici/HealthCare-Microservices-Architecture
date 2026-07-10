using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.InboxQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InboxResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InboxHandlers
{
    public class GetInboxByUserIdQueryHandler : IRequestHandler<GetInboxByUserIdQuery, InternalHandlerResponse<IReadOnlyCollection<GetInboxByUserIdQueryResult>>>
    {
        private readonly IWorkItemService _workItemService;
        private readonly IMapper _mapper;
        private readonly ICacheProvider _cacheProvider;
        private readonly ICurrentUserService _currentUserService;

        public GetInboxByUserIdQueryHandler(IWorkItemService workItemService, IMapper mapper, ICacheProvider cacheProvider, ICurrentUserService currentUserService)
        {
            _workItemService = workItemService;
            _mapper = mapper;
            _cacheProvider = cacheProvider;
            _currentUserService = currentUserService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetInboxByUserIdQueryResult>>> Handle(GetInboxByUserIdQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();

            Expression<Func<WorkItem, bool>> filter = x => (
                (x.AssignedUserId == request.AssignedUserId) &&
                (x.Status == 1)
            );

            Dictionary<Expression<Func<WorkItem, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<WorkItem, object>>, List<Expression<Func<object, object>>>>()
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

            dBQueryOptions.filter = filter;
            dBQueryOptions.thenIncludes = thenIncludes;

            InternalServiceResponse<IReadOnlyCollection<WorkItem>> result = await _workItemService.GetWorkItemByFilterAsync(dBQueryOptions);

            IReadOnlyCollection<GetInboxByUserIdQueryResult> mappedData = _mapper.Map<IReadOnlyCollection<GetInboxByUserIdQueryResult>>(result.Data);

            return InternalHandlerResponse<IReadOnlyCollection<GetInboxByUserIdQueryResult>>.Success(mappedData);
        }
    }
}