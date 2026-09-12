using FluentValidation;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HealthCare.Descriptions.Application.Common.Constants.ValidationConstants;

namespace HealthCare.Descriptions.Application.Features.Validations.Cities
{
    public class CityFilterQueryValidator : AbstractValidator<GetCitiesByFilterQuery>
    {
        private readonly int PlateMinLength = 1;
        private readonly int PlateMaxLength = 99;

        public CityFilterQueryValidator()
        {
            RuleFor(x => x.Plate)
                .InclusiveBetween(PlateMinLength, PlateMaxLength).WithMessage(ValidRange)
                .When(x => x.Plate != null);
        }
    }
}