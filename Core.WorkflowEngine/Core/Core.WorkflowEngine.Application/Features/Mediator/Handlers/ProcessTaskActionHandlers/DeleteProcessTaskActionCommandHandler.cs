using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskActionHandlers
{
    public class DeleteProcessTaskActionCommandHandler : IRequestHandler<DeleteProcessTaskActionCommand, InternalHandlerResponse<bool>>
    {
        private readonly IRepository<ProcessTaskAction> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteProcessTaskActionCommandHandler> _logger;

        public DeleteProcessTaskActionCommandHandler(IRepository<ProcessTaskAction> repository, IMapper mapper, ILogger<DeleteProcessTaskActionCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteProcessTaskActionCommand request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTaskAction> dBQueryOptions = new DBQueryOptions<ProcessTaskAction>();

            Expression<Func<ProcessTaskAction, bool>> filter = x => x.Id == request.Id;
            dBQueryOptions.filter = filter;

            ProcessTaskAction result = await _repository.GetDataAsync(dBQueryOptions);

            if (result != null)
            {
                await _repository.DeleteDataAsync(result);

                return InternalHandlerResponse<bool>.Success(true, InternalHandlerConstants.SuccessProcessTaskActionDeleting);
            }

            return InternalHandlerResponse<bool>.Failure(InternalHandlerConstants.ErrorProcessTaskActionDeleting);
        }
    }
}