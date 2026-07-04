using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using static Core.WorkflowEngine.Application.Features.Constants.ValidationConstants;
using static Core.WorkflowEngine.Application.Features.Constants.ValidationConstants.ProcessTaskActionDefinition;

namespace Core.WorkflowEngine.Application.Features.Mediator.Validations.ProcessTaskActionValidaitons
{
    public class ProcessTaskActionValidator : AbstractValidator<CreateProcessTaskActionCommand>
    {
        public ProcessTaskActionValidator()
        {
            RuleFor(pta => pta.ProcessTaskId).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(pta => pta.ActionName)
                .NotEmpty().WithMessage(NotEmptyMessage)
                .Must(x => x is string).WithMessage(StringTypeofMessage)
                .Length(MinCharLength, MaxCharLength).WithMessage(CharLengthMessage)
                .Must(CheckSpecialChar).WithMessage(SpecialCharMessage);
        }

        private bool CheckSpecialChar(string data)
        {
            return Regex.IsMatch(data, @"^[^@&?*%$#£]*$");
        }
    }
    public class UpdateProcessTaskActionValidator : AbstractValidator<UpdateProcessTaskActionCommand>
    {
        public UpdateProcessTaskActionValidator()
        {
            RuleFor(pta => pta.ProcessTaskId).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(pta => pta.ActionId).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(pta => pta.ActionName)
                .NotEmpty().WithMessage(NotEmptyMessage)
                .Must(x => x is string).WithMessage(StringTypeofMessage)
                .Length(MinCharLength, MaxCharLength).WithMessage(CharLengthMessage)
                .Must(CheckSpecialChar).WithMessage(SpecialCharMessage);
        }

        private bool CheckSpecialChar(string data)
        {
            return Regex.IsMatch(data, @"^[^@&?*%$#£]*$");
        }
    }
}