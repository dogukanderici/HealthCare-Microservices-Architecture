using Core.WebUI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicRoleDtos
{
    public class DynamicRoleQueryDto : IDto
    {
        public Guid dynamicPolicyId { get; set; }
        public Guid allowedRoleId { get; set; }
        public string methode { get; set; }
        public string endpoint { get; set; }
        public bool isActive { get; set; } = true;
    }
}