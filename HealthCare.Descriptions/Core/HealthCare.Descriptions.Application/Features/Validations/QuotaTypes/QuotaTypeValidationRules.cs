using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Validations.QuotaTypes
{
    public static class QuotaTypeValidationRules
    {
        public const int MinNameLength = 2;
        public const int MaxNameLength = 10;
        public const int CodeLength = 5;
        public const string CodeMessage = "Kota Tipi kodu QT ile başlamalı ve kalan 3 karakterin her biri bir rakam olmalıdır!";

        public static bool CheckCode(string quotaTypeCode)
        {
            return Regex.IsMatch(quotaTypeCode, @"^QT[0-9]{3}");
        }

        public static bool CheckSpecialChar(string policlinicName)
        {
            return Regex.IsMatch(policlinicName, @"[^@?&|/\\\^#+$£'<>]*$");
        }
    }
}
