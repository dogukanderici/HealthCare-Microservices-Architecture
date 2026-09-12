using FluentValidation;
using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static HealthCare.Descriptions.Application.Common.Constants.ValidationConstants;
using static HealthCare.Descriptions.Application.Features.Validations.Hospitals.HospitalValidationRules;

namespace HealthCare.Descriptions.Application.Features.Validations.Hospitals
{
    public class HospitalCreateCommandValidator : AbstractValidator<CreateHospitalCommand>
    {
        public HospitalCreateCommandValidator()
        {
            RuleFor(x => x.HospitalCode)
                .NotEmpty().WithMessage(NotEmpty)
                .Must(CheckSpecialChar).WithMessage(NotUseSpecChar)
                .Must(CheckCodeStartWith).WithMessage(CodeMessage);

            RuleFor(x => x.HospitalName)
                .NotEmpty().WithMessage(NotEmpty)
                .Length(MinNameLength, MaxNameLength).WithMessage(ValidTextLength);

            RuleFor(x => x.HospitalCity).NotEmpty().WithMessage(NotEmpty);

            RuleFor(x => x.HospitalDistrict).NotEmpty().WithMessage(NotEmpty);

            RuleFor(x => x.IsAvailable).NotEmpty().WithMessage(NotEmpty);
        }
    }
}