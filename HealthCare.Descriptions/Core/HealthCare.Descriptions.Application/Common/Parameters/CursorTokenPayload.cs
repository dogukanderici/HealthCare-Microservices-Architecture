using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Common.Parameters
{
    public class CursorTokenPayload<T, THandler>
        where THandler : class
    {
        public DateTimeOffset LastCreatedAt { get; set; }
        public T FirstData { get; set; }
        public T LastData { get; set; }
        public int TotalCount { get; set; }
        public int TakenCount { get; set; } = 10;
        public string EntityName { get; set; } = typeof(THandler).Name;
        public bool IsForward { get; set; } = true; // Default olarak isteğin sonraki sayfa için gideceği belirtir.
    }
}