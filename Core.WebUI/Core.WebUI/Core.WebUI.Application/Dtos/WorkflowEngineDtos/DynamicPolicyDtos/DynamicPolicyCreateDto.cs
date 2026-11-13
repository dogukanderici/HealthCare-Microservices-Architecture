using Core.WebUI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicPolicyDtos
{
    public class DynamicPolicyCreateDto : IDto
    {
        public string Methode { get; set; }
        public string Endpoint { get; set; }
        public bool IsActive { get; set; }
    }
}