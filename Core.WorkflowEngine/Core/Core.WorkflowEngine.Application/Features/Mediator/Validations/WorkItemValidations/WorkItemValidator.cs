using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkItemCommands;
using FluentValidation;

using static Core.WorkflowEngine.Application.Commons.Constants.ValidationConstants;

namespace Core.WorkflowEngine.Application.Features.Mediator.Validations.WorkItemValidations
{
    public class WorkItemValidator : AbstractValidator<CreateWorkItemCommand>
    {
        public WorkItemValidator()
        {
            RuleFor(w => w.InstanceId).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(w => w.AssignedUserId).NotEmpty().WithMessage(NotEmptyMessage);
        }
    }

    public class UpdateWorkItemValidator : AbstractValidator<UpdateWorkItemCommand>
    {
        public UpdateWorkItemValidator()
        {
            RuleFor(w => w.InstanceId).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(w => w.AssignedUserId).NotEmpty().WithMessage(NotEmptyMessage);
        }
    }
}
