using FluentValidation;
using HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Queries;
using Microsoft.AspNetCore.SignalR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HealthCare.Descriptions.Application.Common.Constants.ValidationConstants;
using static HealthCare.Descriptions.Application.Features.Validations.ServiceTypes.ServiceTypeValidationRules;

namespace HealthCare.Descriptions.Application.Features.Validations.ServiceTypes
{
    public class ServiceTypeFilterQueryValidator : AbstractValidator<GetServiceTypesByFilterQuery>
    {
        public ServiceTypeFilterQueryValidator()
        {
            RuleFor(x => x.ServiceCode)
                .Length(CodeLength).WithMessage(ValidLength)
                .Must(CheckCode).WithMessage(CodeMessage)
                .Must(CheckSpecialChar).WithMessage(NotUseSpecChar)
                .When(x => x.ServiceCode != null);

            RuleFor(x => x.ServiceName)
                .Length(MinNameLength, MaxNameLength).WithMessage(ValidTextLength)
                .Must(CheckSpecialChar).WithMessage(NotUseSpecChar)
                .When(x => x.ServiceName != null);
        }
    }
}