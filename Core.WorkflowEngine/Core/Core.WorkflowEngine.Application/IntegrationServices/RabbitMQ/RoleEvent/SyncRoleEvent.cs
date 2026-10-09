using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.IntegrationServices.RabbitMQ.RoleEvent
{
    public class SyncRoleEvent : IRabbitMQEvent
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}