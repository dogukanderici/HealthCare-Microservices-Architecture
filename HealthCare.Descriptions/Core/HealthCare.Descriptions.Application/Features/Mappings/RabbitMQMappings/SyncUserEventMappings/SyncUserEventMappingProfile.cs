using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.UserEvent.Commands;
using HealthCare.Descriptions.Application.IntegrationServices.RabbitMQ;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mappings.RabbitMQMappings.SyncUserEventMappings
{
    public class SyncUserEventMappingProfile : Profile
    {
        public SyncUserEventMappingProfile()
        {
            CreateMap<SyncedUser, SyncUserEvent>().ReverseMap();
            CreateMap<SyncedUser, CreateUserEventCommand>().ReverseMap();
            CreateMap<SyncedUser, UpdateUserEventCommand>().ReverseMap();
        }
    }
}