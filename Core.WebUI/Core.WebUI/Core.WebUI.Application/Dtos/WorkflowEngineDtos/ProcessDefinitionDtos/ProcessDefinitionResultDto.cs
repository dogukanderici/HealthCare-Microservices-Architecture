using Core.WebUI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessDefinitionDtos
{
    public class ProcessDefinitionResultDto : IDto
    {
        public Guid Id { get; set; }
        public Guid ProcessSpecId { get; set; }
        public string ProcessName { get; set; }
        public bool IsActive { get; set; }
    }
}