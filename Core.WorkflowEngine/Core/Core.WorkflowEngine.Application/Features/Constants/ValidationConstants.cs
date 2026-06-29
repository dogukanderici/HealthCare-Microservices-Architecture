using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Constants
{
    public class ValidationConstants
    {
        public static readonly string NotEmptyMessage = "{PropertyName} Gönderilen Değer Boş Olamaz.";
        public static readonly string GuidTypeofMessage = "{PropertyName} Gönderilen Değer Guid (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx) Değerinde Olmalıdır.";
        public static readonly string StringTypeofMessage = "{PropertyName} Gönderilen Değer Metin Değerinde Olmalıdır.";
        public static readonly string BooleanTypeofMessage = "{PropertyName} Gönderilen Değer İkili Değer (true veya false) Değerinde Olmalıdır.";
        public static readonly string SpecialCharMessage = "{PropertyName} Gönderilen Değer Özel Karakterler İçeremez";

        public static class ProcessDefinition
        {
            public const int MinCharLength = 5;
            public const int MaxCharLength = 255;
            public static readonly string CharLengthMessage = $"En Az {MinCharLength} ve En Fazla {MaxCharLength} Karakter Olmalıdır.";

        }

        public static class ProcessTaskDefinition
        {
            public const int MinCharLength = 3;
            public const int MaxCharLength = 255;
            public static readonly string CharLengthMessage = $"En Az {MinCharLength} ve En Fazla {MaxCharLength} Karakter Olmalıdır.";

        }

        public static class ProcessTaskActionDefinition
        {
            public const int MinCharLength = 2;
            public const int MaxCharLength = 255;
            public static readonly string CharLengthMessage = $"En Az {MinCharLength} ve En Fazla {MaxCharLength} Karakter Olmalıdır.";

        }
    }
}
