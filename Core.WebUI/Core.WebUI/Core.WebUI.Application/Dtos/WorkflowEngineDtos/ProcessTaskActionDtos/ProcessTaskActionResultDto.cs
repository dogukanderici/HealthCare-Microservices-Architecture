using Core.WebUI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessTaskActionDtos
{
    public class ProcessTaskActionResultDto : IDto
    {
        public Guid Id { get; set; }
        public Guid ProcessTaskId { get; set; }
        public Guid ActionId { get; set; }
        public string ActionName { get; set; }
        public int ActionType { get; set; } = 1;
        public int ExecutionOrder { get; set; }
        public bool IsActive { get; set; }
    }
}