using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Validations.Hospitals
{
    public static class HospitalValidationRules
    {
        public const int MinNameLength = 5;
        public const int MaxNameLength = 99;
        public const string CodeMessage = "Hastane Kodu H ile başlamalı ve kalan 5 karakter her biri bir rakam olmalıdır!";

        public static bool CheckCodeStartWith(string hospitalCode)
        {
            return Regex.IsMatch(hospitalCode, @"^H[0-9]{5}");
        }

        public static bool CheckSpecialChar(string hospitalName)
        {
            return Regex.IsMatch(hospitalName, @"^[^@?&|/\\\^#+$£'<>]*$");
        }
    }
}