using Core.WebUI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Dtos.IdentityDtos.RoleDtos
{
    public class RoleUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}