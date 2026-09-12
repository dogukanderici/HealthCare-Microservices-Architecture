using FluentValidation;
using HealthCare.Descriptions.Application.Features.Mediators.Districts.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HealthCare.Descriptions.Application.Common.Constants.ValidationConstants;
using static HealthCare.Descriptions.Application.Features.Validations.Districts.DistrictValidationRules;

namespace HealthCare.Descriptions.Application.Features.Validations.Districts
{
    public class DistrictFilterQueryValidator : AbstractValidator<GetDistrictsByFilterQuery>
    {
        public DistrictFilterQueryValidator()
        {
            RuleFor(x => x.Plate)
                .InclusiveBetween(PlateMinLength, PlateMaxLength).WithMessage(ValidRange)
                .When(x => x.Plate != null);
        }
    }
}