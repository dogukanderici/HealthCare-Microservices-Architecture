using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.ServiceDtos.ProcessDefinitionDtos
{
    public class ProcessDefinitionFilterDto
    {
        public Guid? ProcessSpecId { get; set; }
        public string? ProcessName { get; set; }
        public bool? IsActive { get; set; }
    }
}