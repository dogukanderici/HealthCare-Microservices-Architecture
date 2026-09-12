using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Validations.Policlinics
{
    public static class PoliclinicValidationRules
    {
        public const int MinNameLength = 5;
        public const int MaxNameLength = 25;
        public const int CodeLength = 6;
        public const string CodeMessage = "Poliklinik Kodu P ile başlamalıdır ve kalan 5 karakter her biri bir rakam olmalıdır!";

        public static bool CheckName(string policlinicCode)
        {
            return Regex.IsMatch(policlinicCode, @"^P[0-9]{5}");
        }

        public static bool CheckSpecialChar(string policlinicName)
        {
            return Regex.IsMatch(policlinicName, @"[^@?&|/\\\^#+$£'<>]*$");
        }
    }
}
