using Core.WebUI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessDefinitionDtos
{
    public class ProcessDefinitionCreateDto : IDto
    {
        public string ProcessName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}