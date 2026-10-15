using HealthCare.Descriptions.Application.Interfaces.IntegrationServices.RabbitMQ;
using HealthCare.Descriptions.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.IntegrationServices.RabbitMQ
{
    public class SyncUserEvent : IRabbitMQEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string EMail { get; set; }
        public bool IsAvailable { get; set; }
    }
}