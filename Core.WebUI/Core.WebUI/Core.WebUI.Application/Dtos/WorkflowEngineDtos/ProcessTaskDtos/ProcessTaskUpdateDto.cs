using Core.WebUI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessTaskDtos
{
    public class ProcessTaskUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public Guid ProcessId { get; set; }
        public string StepName { get; set; }
        public Guid AssignedUser { get; set; }
        public bool IsStartStep { get; set; }
        public bool IsActive { get; set; }
    }
}
