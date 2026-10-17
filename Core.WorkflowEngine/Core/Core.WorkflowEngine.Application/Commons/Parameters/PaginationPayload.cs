using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Commons.Parameters
{
    public class PaginationPayload<T>
    {
        public DateTimeOffset LastCreatedAt { get; set; }
        public T FirstData { get; set; }
        public T LastData { get; set; }
        public int TotalCount { get; set; }
        public int TakenCount { get; set; } = 10;
        public string EntityName { get; set; }
        public bool IsForward { get; set; } = true; // Default olarak bir sonraki sayfaya geçeceğini belirtir.
    }
}