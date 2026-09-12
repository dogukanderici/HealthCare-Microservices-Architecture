using FluentValidation;
using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HealthCare.Descriptions.Application.Common.Constants.ValidationConstants;
using static HealthCare.Descriptions.Application.Features.Validations.Hospitals.HospitalValidationRules;

namespace HealthCare.Descriptions.Application.Features.Validations.Hospitals
{
    public class HospitalFilyerQueryValidator : AbstractValidator<GetHospitalsByFilterQuery>
    {
        public HospitalFilyerQueryValidator()
        {
            RuleFor(x => x.Code)
                .Must(CheckSpecialChar).WithMessage("Hastane Kodunda özel karakter kullanılamaz!")
                .Must(CheckCodeStartWith).WithMessage("Hastane Kodu H ile başlamalı ve kalan 5 karakter her biri bir rakam olmalıdır!")
                .When(x => x.Code != null);
        }
    }
}