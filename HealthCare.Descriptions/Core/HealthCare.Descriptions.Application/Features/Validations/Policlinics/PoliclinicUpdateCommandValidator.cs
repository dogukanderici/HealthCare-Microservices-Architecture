using FluentValidation;
using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HealthCare.Descriptions.Application.Common.Constants.ValidationConstants;
using static HealthCare.Descriptions.Application.Features.Validations.Policlinics.PoliclinicValidationRules;

namespace HealthCare.Descriptions.Application.Features.Validations.Policlinics
{
    public class PoliclinicUpdateCommandValidator : AbstractValidator<UpdatePoliclinicCommand>
    {
        public PoliclinicUpdateCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.PoliclinicCode)
                .NotEmpty().WithMessage(NotEmpty)
                .Length(CodeLength).WithMessage(ValidLength)
                .Must(CheckSpecialChar).WithMessage(NotUseSpecChar)
                .Must(CheckName).WithMessage(CodeMessage);

            RuleFor(x => x.PoliclinicName)
                .NotEmpty().WithMessage(NotEmpty)
                .Length(MinNameLength, MaxNameLength).WithMessage(ValidTextLength)
                .Must(CheckSpecialChar).WithMessage(NotUseSpecChar);

            RuleFor(x => x.IsAvailable).NotEmpty().WithMessage(NotEmpty);
        }
    }
}
