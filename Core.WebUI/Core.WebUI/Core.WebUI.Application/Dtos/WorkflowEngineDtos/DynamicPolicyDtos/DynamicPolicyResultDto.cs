using Core.WebUI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicPolicyDtos
{
    public class DynamicPolicyResultDto : IDto
    {
        public Guid Id { get; set; }
        public string Methode { get; set; }
        public string Endpoint { get; set; }
        public bool IsActive { get; set; }
    }
}