using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.IntegrationServices.RabbitMQ.UserEvent
{
    public class SyncUserEvent : IRabbitMQEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAvailable { get; set; }
    }
}