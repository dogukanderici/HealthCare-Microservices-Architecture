using FluentValidation;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HealthCare.Descriptions.Application.Common.Constants.ValidationConstants;
using static HealthCare.Descriptions.Application.Features.Validations.Cities.CityValidationRules;

namespace HealthCare.Descriptions.Application.Features.Validations.Cities
{
    public class CityCreateCommandValidator : AbstractValidator<CreateCityCommand>
    {
        public CityCreateCommandValidator()
        {
            RuleFor(x => x.Plate)
                .NotEmpty().WithMessage(NotEmpty)
                .InclusiveBetween(PlateMinLength, PlateMaxLength).WithMessage(ValidRange);

            RuleFor(x => x.CityName)
                .NotEmpty().WithMessage(NotEmpty)
                .Length(MinLength, MaxLength).WithMessage(ValidTextLength);

            RuleFor(x => x.IsAvailable).NotEmpty().WithMessage(NotEmpty);
        }
    }
}