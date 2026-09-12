using FluentValidation;
using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Queries;
using Microsoft.AspNetCore.SignalR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HealthCare.Descriptions.Application.Common.Constants.ValidationConstants;
using static HealthCare.Descriptions.Application.Features.Validations.Policlinics.PoliclinicValidationRules;

namespace HealthCare.Descriptions.Application.Features.Validations.Policlinics
{
    public class PoliclinicFilterQueryValidator : AbstractValidator<GetPoliclinicsByFilterQuery>
    {
        public PoliclinicFilterQueryValidator()
        {
            RuleFor(x => x.Code)
                .Length(CodeLength).WithMessage(ValidLength)
                .Must(CheckSpecialChar).WithMessage(NotUseSpecChar)
                .Must(CheckName).WithMessage(CodeMessage);
        }
    }
}