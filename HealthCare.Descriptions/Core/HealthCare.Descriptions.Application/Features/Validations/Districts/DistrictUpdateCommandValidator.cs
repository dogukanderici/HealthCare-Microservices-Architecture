using FluentValidation;
using HealthCare.Descriptions.Application.Features.Mediators.Districts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HealthCare.Descriptions.Application.Common.Constants.ValidationConstants;
using static HealthCare.Descriptions.Application.Features.Validations.Districts.DistrictValidationRules;

namespace HealthCare.Descriptions.Application.Features.Validations.Districts
{
    public class DistrictUpdateCommandValidator : AbstractValidator<UpdateDistrictCommand>
    {

        public DistrictUpdateCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(NotEmpty);

            RuleFor(x => x.DistrictName)
                .NotEmpty().WithMessage(NotEmpty)
                .Length(DisctrictMinLength, DistrictMaxLength).WithMessage(ValidTextLength);
        }
    }
}