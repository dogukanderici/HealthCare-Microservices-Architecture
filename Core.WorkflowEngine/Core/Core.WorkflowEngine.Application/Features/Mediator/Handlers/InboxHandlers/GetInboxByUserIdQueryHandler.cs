using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.InboxQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InboxResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
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
    public class GetInboxByUserIdQueryHandler : IRequestHandler<GetInboxByUserIdQuery, InternalHandlerResponse<List<GetInboxByUserIdQueryResult>>>
    {
        private readonly IRepository<WorkItem> _workItemRepository;
        private readonly IMapper _mapper;

        public GetInboxByUserIdQueryHandler(IRepository<WorkItem> workItemRepository, IMapper mapper)
        {
            _workItemRepository = workItemRepository;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<List<GetInboxByUserIdQueryResult>>> Handle(GetInboxByUserIdQuery request, CancellationToken cancellationToken)
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

            List<WorkItem> result = await _workItemRepository.GetAllDataAsync(dBQueryOptions);

            return InternalHandlerResponse<List<GetInboxByUserIdQueryResult>>.Success(_mapper.Map<List<GetInboxByUserIdQueryResult>>(result));
        }
    }
}