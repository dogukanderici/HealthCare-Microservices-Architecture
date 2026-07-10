using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.ServiceDtos.WorkItemServiceDtos
{
    public class WorkItemFilterDto
    {
        public Guid? InstanceId { get; set; }
        public Guid? WorkItemId { get; set; }
        public Guid? AssignedUserId { get; set; }
        public int? Status { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
    }
}