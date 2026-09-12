using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Common.Constants
{
    public static class ValidationConstants
    {
        public static readonly string NotEmpty = "Bu Alan Boş Bırakılamaz. ({PropertyName})";
        public static readonly string StringType = "Bu Alan Metin Tipinde Olmalıdır. ({PropertyName})";
        public static readonly string BooleanType = "Bu Alan İkili Değer Tipinde Olmalıdır. ({PropertyName})";
        public static readonly string IntegerType = "Bu Alan Tam Sayı Tipinde Olmalıdır. ({PropertyName})";
        public static readonly string LessThanZero = "Bu Alan Sıfırdan Küçük Olamaz. ({PropertyName})";
        public static readonly string GreaterThanZero = "Bu Alan Sıfırdan Büyük Olamaz. ({PropertyName})";
        public static readonly string ValidEmail = "Geçerli Bir Email Giriniz. ({PropertyName})";
        public static readonly string ValidTextLength = "Metin Uzunluğu Belirtilen Değerler Aralığında Olmalıdır. ({MinLength} - {MaxLength}). ({PropertyName})";
    }
}