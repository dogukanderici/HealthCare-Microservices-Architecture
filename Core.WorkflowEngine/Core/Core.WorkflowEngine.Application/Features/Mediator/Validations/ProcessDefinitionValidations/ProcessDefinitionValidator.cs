using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessDefinitionCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using static Core.WorkflowEngine.Application.Features.Constants.ValidationConstants.ProcessDefinition;

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
            return Regex.IsMatch(data, @"^[^&?*]*$");
        }
    }
}