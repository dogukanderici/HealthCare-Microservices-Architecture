using FluentValidation;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HealthCare.Descriptions.Application.Common.Constants.ValidationConstants;

namespace HealthCare.Descriptions.Application.Features.Validations.Appointments
{
    public class UpdateAppointmentStatusCommandValidator : AbstractValidator<UpdateAppointmentStatusCommand>
    {
        private readonly int MinLength = 5;
        private readonly int MaxLength = 255;

        public UpdateAppointmentStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(NotEmpty);

            RuleFor(x => x.StatusName)
                .NotEmpty().WithMessage(NotEmpty)
                .Length(MinLength, MaxLength).WithMessage(ValidTextLength);

            RuleFor(x => x.IsAvailable)
                .NotEmpty().WithMessage(NotEmpty);
        }
    }
}