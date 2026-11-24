using Core.WebUI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicPolicyDtos
{
    public class DynamicPolicyQueryDto : IDto
    {
        public string Methode { get; set; }
    }
}