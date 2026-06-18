using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.AppointmentStatusCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Results.AppointmentStatusResults;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.MappingProfiles.AppointmentStatusMappings
{
    public class AppointmentStatusMappingProfile : Profile
    {
        public AppointmentStatusMappingProfile()
        {
            CreateMap<AppointmentStatus, GetAppointmentStatusesQueryResult>().ReverseMap();
            CreateMap<AppointmentStatus, GetAppointmentStatusByIdQueryResult>().ReverseMap();
            CreateMap<AppointmentStatus, CreateAppointmentStatusCommand>().ReverseMap();
            CreateMap<AppointmentStatus, UpdateAppointmentStatusCommand>().ReverseMap();
        }
    }
}
