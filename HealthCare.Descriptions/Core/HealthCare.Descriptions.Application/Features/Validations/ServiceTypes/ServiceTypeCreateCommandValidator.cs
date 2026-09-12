using FluentValidation;
using HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HealthCare.Descriptions.Application.Common.Constants.ValidationConstants;
using static HealthCare.Descriptions.Application.Features.Validations.ServiceTypes.ServiceTypeValidationRules;

namespace HealthCare.Descriptions.Application.Features.Validations.ServiceTypes
{
    public class ServiceTypeCreateCommandValidator : AbstractValidator<CreateServiceTypeCommand>
    {
        public ServiceTypeCreateCommandValidator()
        {
            RuleFor(x => x.ServiceCode)
                .NotEmpty().WithMessage(NotEmpty)
                .Length(CodeLength).WithMessage(ValidLength)
                .Must(CheckCode).WithMessage(CodeMessage)
                .Must(CheckSpecialChar).WithMessage(NotUseSpecChar);

            RuleFor(x => x.ServiceName)
                .NotEmpty().WithMessage(NotEmpty)
                .Length(MinNameLength, MaxNameLength).WithMessage(ValidTextLength)
                .Must(CheckSpecialChar).WithMessage(NotUseSpecChar);

            RuleFor(x => x.IsAvailable).NotEmpty().WithMessage(NotEmpty);
        }
    }
}
