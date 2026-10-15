using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.RoleEvent.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.RoleEvent.Results.Shared;
using HealthCare.Descriptions.Application.IntegrationServices.RabbitMQ;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mappings.RabbitMQMappings.SyncRoleEventMappings
{
    public class SyncedRoleMappingProfile : Profile
    {
        public SyncedRoleMappingProfile()
        {
            CreateMap<SyncedRole, SyncRoleEvent>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsAvailable))
                .ReverseMap();

            CreateMap<SyncedRole, CreateSyncedRoleCommand>().ReverseMap();
            CreateMap<SyncedRole, UpdateSyncedRoleCommand>().ReverseMap();

            // For Relations
            CreateMap<SyncedRole, SharedSyncedRoleResult>().ReverseMap();
        }
    }
}