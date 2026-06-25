using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.ServiceDtos.ProcessTaskTransitionDtos
{
    public class TaskTransitionFilterDto
    {
        public Guid? ProcessTaskId { get; set; }
        public Guid? NextTaskId { get; set; }
        public Guid? ActionId { get; set; }
        public bool? IsActive { get; set; }
    }
}