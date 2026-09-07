using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands;
using FluentValidation;
using System.Text.RegularExpressions;

using static Core.WorkflowEngine.Application.Commons.Constants.ValidationConstants;
using static Core.WorkflowEngine.Application.Commons.Constants.ValidationConstants.ProcessTaskActionDefinition;

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