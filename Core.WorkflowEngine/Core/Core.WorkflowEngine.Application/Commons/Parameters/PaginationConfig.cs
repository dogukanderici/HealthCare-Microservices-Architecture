using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Core.WorkflowEngine.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Commons.Parameters
{
    public class PaginationConfig<TEntity, TPropertType, TDto>
        where TEntity : class, IEntity
    {
        public string Token { get; set; }
        public int Take { get; set; } = 10;

        // Doğru filtreleme yapabilmesi için OrderBy ve Cursor aynı tipte olmalı.
        public Expression<Func<TEntity, object>> OrderBy { get; set; }
        public Func<TPropertType, Expression<Func<TEntity, bool>>> ForwardFilter { get; set; } // Sonraki sayfa verileri için kullanılacak filtreleme.
        public Func<TPropertType, Expression<Func<TEntity, bool>>> BackwardFilter { get; set; } // Önceki sayfa için kullanılacak filtreleme
        public Func<TDto, TPropertType> CursorSelector { get; set; } // Sayfada gösterilecek ilk ve son veriyi bulmak için kullanılır.
        public Func<TDto, DateTimeOffset> CreatedAtCursor { get; set; } // Sayfadaki son verinin oluşturulma tarihi verisi.
        public Func<DBQueryOptions<TEntity>?, Task<int>> GetTotalCountAsync { get; set; } // Kayıtlı toplam veriyi bulmak kullanılır.
        public Func<DBQueryOptions<TEntity>, Task<InternalHandlerResponse<IReadOnlyCollection<TDto>>>> FetchDataAsync { get; set; } // Verileri DB'den getirecek metot.
    }
}