using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessDefinitionCommands;
using FluentValidation;
using System.Text.RegularExpressions;

using static Core.WorkflowEngine.Application.Commons.Constants.ValidationConstants;
using static Core.WorkflowEngine.Application.Commons.Constants.ValidationConstants.ProcessDefinition;

namespace Core.WorkflowEngine.Application.Features.Mediator.Validations.ProcessDefinitionValidations
{
    public class ProcessDefinitionValidator : AbstractValidator<CreateProcessDefinitionCommand>
    {
        public ProcessDefinitionValidator()
        {
            RuleFor(x => x.ProcessName)
                .NotEmpty().WithMessage(NotEmptyMessage)
                .Must(p => p is string).WithMessage(StringTypeofMessage)
                .Must(CheckSpecialChar).WithMessage(SpecialCharMessage)
                .Length(MinCharLength, MaxCharLength).WithMessage(CharLengthMessage);

            RuleFor(x => x.IsActive)
                .NotEmpty().WithMessage(NotEmptyMessage)
                .Must(p => p is bool).WithMessage(BooleanTypeofMessage);
        }

        private bool CheckSpecialChar(string data)
        {
            return Regex.IsMatch(data, @"^[^@&?*%$#£]*$");
        }
    }

    public class UpdateProcessDefinitionValidator : AbstractValidator<UpdateProcessDefinitionCommand>
    {
        public UpdateProcessDefinitionValidator()
        {
            RuleFor(x => x.ProcessName)
                .NotEmpty().WithMessage(NotEmptyMessage)
                .Must(p => p is string).WithMessage(StringTypeofMessage)
                .Must(CheckSpecialChar).WithMessage(SpecialCharMessage)
                .Length(MinCharLength, MaxCharLength).WithMessage(CharLengthMessage);

            RuleFor(x => x.IsActive)
                .NotEmpty().WithMessage(NotEmptyMessage)
                .Must(p => p is bool).WithMessage(BooleanTypeofMessage);
        }

        private bool CheckSpecialChar(string data)
        {
            return Regex.IsMatch(data, @"^[^@&?*%$#£]*$");
        }
    }
}