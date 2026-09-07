using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessDefinitionCommands
{
    public class CreateProcessDefinitionCommand : IRequest<InternalHandlerResponse<Guid>>, ITransactionalRequest
    {
        public string ProcessName { get; set; }
        public bool IsActive { get; set; }
    }
}