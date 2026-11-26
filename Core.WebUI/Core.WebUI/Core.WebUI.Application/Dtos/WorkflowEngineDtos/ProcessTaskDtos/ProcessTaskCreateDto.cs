using Core.WebUI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessTaskDtos
{
    public class ProcessTaskCreateDto : IDto
    {
        public Guid ProcessId { get; set; }
        public string StepName { get; set; }
        public Guid AssignedUser { get; set; }
        public bool IsStartStep { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
