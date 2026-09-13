using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Validations.HospitalPoliclinicQuotas
{
    public static class HospitalPoliclinicQuotaValidationRules
    {
        public static DateTimeOffset CurrentDate = DateTimeOffset.Now;
        public static string DateMessage = "Geçerlilik Tarihi Bugünden Küçük Olamaz!";

        public static bool CheckValidityDate(DateTimeOffset date)
        {
            return date > CurrentDate;
        }

        public static bool CheckQuota(int quota)
        {
            return quota > 0;
        }
    }
}
