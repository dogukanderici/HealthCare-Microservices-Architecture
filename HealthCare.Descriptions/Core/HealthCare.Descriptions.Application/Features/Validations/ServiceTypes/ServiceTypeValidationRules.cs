using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Validations.ServiceTypes
{
    public static class ServiceTypeValidationRules
    {
        public const int MinNameLength = 5;
        public const int MaxNameLength = 50;
        public const int CodeLength = 5;
        public const string CodeMessage = "Kota Tipi kodu ST ile başlamalı ve kalan 3 karakterin her biri bir rakam olmalıdır!";

        public static bool CheckCode(string quotaTypeCode)
        {
            return Regex.IsMatch(quotaTypeCode, @"^ST[0-9]{3}");
        }

        public static bool CheckSpecialChar(string policlinicName)
        {
            return Regex.IsMatch(policlinicName, @"[^@?&|/\\\^#+$£'<>]*$");
        }
    }
}