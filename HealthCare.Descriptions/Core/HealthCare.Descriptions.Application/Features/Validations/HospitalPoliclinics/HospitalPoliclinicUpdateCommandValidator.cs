using FluentValidation;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HealthCare.Descriptions.Application.Common.Constants.ValidationConstants;

namespace HealthCare.Descriptions.Application.Features.Validations.HospitalPoliclinics
{
    public class HospitalPoliclinicUpdateCommandValidator : AbstractValidator<UpdateHospitalPoliclinicCommand>
    {
        public HospitalPoliclinicUpdateCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(NotEmpty);
            RuleFor(x => x.HospitalId).NotEmpty().WithMessage(NotEmpty);
            RuleFor(x => x.PoliclinicId).NotEmpty().WithMessage(NotEmpty);
            RuleFor(x => x.IsAvailable).NotEmpty().WithMessage(NotEmpty);
        }
    }
}