using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskCommands;
using FluentValidation;
using System.Text.RegularExpressions;

using static Core.WorkflowEngine.Application.Commons.Constants.ValidationConstants;
using static Core.WorkflowEngine.Application.Commons.Constants.ValidationConstants.ProcessTaskDefinition;

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