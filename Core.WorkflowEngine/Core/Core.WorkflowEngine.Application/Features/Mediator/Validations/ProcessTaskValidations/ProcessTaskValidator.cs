using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using static Core.WorkflowEngine.Application.Features.Constants.ValidationConstants;
using static Core.WorkflowEngine.Application.Features.Constants.ValidationConstants.ProcessTaskDefinition;

namespace Core.WorkflowEngine.Application.Features.Mediator.Validations.ProcessTaskValidations
{
    public class ProcessTaskValidator : AbstractValidator<CreateProcessTaskCommand>
    {
        public ProcessTaskValidator()
        {
            RuleFor(pt => pt.ProcessId).NotEmpty().WithMessage(NotEmptyMessage);

            RuleFor(pt => pt.StepName).NotEmpty().WithMessage(NotEmptyMessage)
                .Must(pt => pt is string).WithMessage(StringTypeofMessage)
                .Length(MinCharLength, MaxCharLength).WithMessage(CharLengthMessage)
                .Must(CheckSpecialChar).WithMessage(SpecialCharMessage);
        }

        private bool CheckSpecialChar(string data)
        {
            return Regex.IsMatch(data, @"^[^@&?*%$#£]*$");
        }
    }

    public class UpdateProcessTaskValidator : AbstractValidator<UpdateProcessTaskCommand>
    {
        public UpdateProcessTaskValidator()
        {
            RuleFor(pt => pt.ProcessId).NotEmpty().WithMessage(NotEmptyMessage);

            RuleFor(pt => pt.StepName).NotEmpty().WithMessage(NotEmptyMessage)
                .Must(pt => pt is string).WithMessage(StringTypeofMessage)
                .Length(MinCharLength, MaxCharLength).WithMessage(CharLengthMessage)
                .Must(CheckSpecialChar).WithMessage(SpecialCharMessage);
        }

        private bool CheckSpecialChar(string data)
        {
            return Regex.IsMatch(data, @"^[^@&?*%$#£]*$");
        }
    }
}