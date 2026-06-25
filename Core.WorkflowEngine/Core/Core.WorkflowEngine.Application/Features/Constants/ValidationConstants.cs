using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Constants
{
    public class ValidationConstants
    {
        public static class ProcessDefinition
        {
            public const int MinCharLength = 5;
            public const int MaxCharLength = 255;
            public static readonly string CharLengthMessage = $"En Az {MinCharLength} ve En Fazla {MaxCharLength} Karakter Olmalıdır.";
            public static readonly string NotEmptyMessage = "Gönderilen Değer Boş Olamaz.";
            public static readonly string StringTypeofMessage = "Gönderilen Değer Metin Değerinde Olmalıdır.";
            public static readonly string BooleanTypeofMessage = "Gönderilen Değer İkili Değer (true veya false) Değerinde Olmalıdır.";
            public static readonly string SpecialCharMessage = "Gönderilen Değer Özel Karakterler (&,?,*) İçeremez";

        }
    }
}
