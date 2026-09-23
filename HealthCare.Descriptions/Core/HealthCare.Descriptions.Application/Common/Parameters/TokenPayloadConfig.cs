using HealthCare.Descriptions.Application.Common.Wrappers;
using System.Linq.Expressions;

namespace HealthCare.Descriptions.Application.Common.Parameters
{
    public class TokenPayloadConfig<T, THandler, TDto, TPropType>
        where T : class
    {
        public string Token { get; set; }
        public int Take { get; set; } = 10;

        // Doğru şekilde sayfalama yapılabilmesi için OrderBy ile Cursor aynı tipte olmalı. (int-uuid olamaz. int-int veya uuid-uuid olmalı.)
        public Expression<Func<T, object>> OrderBy { get; set; } // Sıralama yapılacak sütun.
        public Func<TPropType, Expression<Func<T, bool>>> ForwardFilter { get; set; } // İleriye doğru sayfalama yaparken kullanılacak filtreleme.
        public Func<TPropType, Expression<Func<T, bool>>> BackwardFilter { get; set; } // Geriye doğru sayfalama yaparken kullanılacak filtreleme.

        public Func<TDto, TPropType> CursorSelector { get; set; } // ilk veri ve son veriyi bulmak için.
        public Func<TDto, DateTimeOffset>? CreatedAtSelector { get; set; } // Son verinin createdat değerini bulmak için.

        public Func<DBQueryOptions<T>?, Task<int>> GetTotalCountAsync { get; set; } // parametre almayan sadece int değer dönen toplam veri sayısının bulan metot.

        // DBQueryOptions alıp geriye bu query'e ait sonucu dönen metot.
        public Func<DBQueryOptions<T>, Task<InternalServiceResponse<IReadOnlyCollection<TDto>>>> FetchDataAsync { get; set; }
    }
}