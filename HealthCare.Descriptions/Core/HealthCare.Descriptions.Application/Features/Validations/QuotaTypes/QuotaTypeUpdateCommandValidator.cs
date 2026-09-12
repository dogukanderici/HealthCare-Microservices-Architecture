using FluentValidation;
using HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HealthCare.Descriptions.Application.Common.Constants.ValidationConstants;
using static HealthCare.Descriptions.Application.Features.Validations.QuotaTypes.QuotaTypeValidationRules;

namespace HealthCare.Descriptions.Application.Features.Validations.QuotaTypes
{
    public class QuotaTypeUpdateCommandValidator : AbstractValidator<UpdateQuotaTypeCommand>
    {
        public QuotaTypeUpdateCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(NotEmpty);

            RuleFor(x => x.QuotaTypeCode)
                .NotEmpty().WithMessage(NotEmpty)
                .Length(CodeLength).WithMessage(ValidLength)
                .Must(CheckCode).WithMessage(CodeMessage);

            RuleFor(x => x.QuotaTypeName)
                .NotEmpty().WithMessage(NotEmpty)
                .Length(MinNameLength, MaxNameLength).WithMessage(ValidTextLength)
                .Must(CheckSpecialChar).WithMessage(NotUseSpecChar);

            RuleFor(x => x.IsAvailable).NotEmpty().WithMessage(NotEmpty);
        }
    }
}