using FluentValidation;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HealthCare.Descriptions.Application.Common.Constants.ValidationConstants;
using static HealthCare.Descriptions.Application.Features.Validations.HospitalPoliclinicQuotas.HospitalPoliclinicQuotaValidationRules;

namespace HealthCare.Descriptions.Application.Features.Validations.HospitalPoliclinicQuotas
{
    public class HospitalPoliclinicQuotaCreateCommandValidator : AbstractValidator<CreateHospitalPoliclinicQuotaCommand>
    {
        public HospitalPoliclinicQuotaCreateCommandValidator()
        {

            RuleFor(x => x.HospitalPoliclinicId).NotEmpty().WithMessage(NotEmpty);

            RuleFor(x => x.QuotaTypeId).NotEmpty().WithMessage(NotEmpty);

            RuleFor(x => x.Quota)
                .NotEmpty().WithMessage(NotEmpty)
                .Must(CheckQuota).WithMessage(LessThanZero);

            RuleFor(x => x.ValidityDate)
                .NotEmpty().WithMessage(NotEmpty)
                .Must(CheckValidityDate).WithMessage(DateMessage);

            RuleFor(x => x.IsAvailable).NotEmpty().WithMessage(NotEmpty);
        }
    }
}