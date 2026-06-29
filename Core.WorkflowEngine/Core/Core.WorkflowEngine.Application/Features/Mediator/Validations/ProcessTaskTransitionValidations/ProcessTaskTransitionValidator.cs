using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskTransitionCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Core.WorkflowEngine.Application.Features.Constants.ValidationConstants;

namespace Core.WorkflowEngine.Application.Features.Mediator.Validations.ProcessTaskTransitionValidations
{
    public class ProcessTaskTransitionValidator : AbstractValidator<CreateProcessTaskTransitionCommand>
    {
        public ProcessTaskTransitionValidator()
        {
            RuleFor(ptt => ptt.ProcessTaskId).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(ptt => ptt.NextTaskId).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(ptt => ptt.ActionId).NotEmpty().WithMessage(NotEmptyMessage);
        }
    }

    public class UpdateProcessTaskTransitionValidator : AbstractValidator<UpdateProcessTaskTransitionCommand>
    {
        public UpdateProcessTaskTransitionValidator()
        {
            RuleFor(ptt => ptt.ProcessTaskId).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(ptt => ptt.NextTaskId).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(ptt => ptt.ActionId).NotEmpty().WithMessage(NotEmptyMessage);
        }
    }
}