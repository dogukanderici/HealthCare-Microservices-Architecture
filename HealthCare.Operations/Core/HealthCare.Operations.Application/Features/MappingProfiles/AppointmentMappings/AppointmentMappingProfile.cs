using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Commands.AppoinmentCommands;
using HealthCare.Operations.Application.Features.Mediator.Results.AppoinmentResults;
using HealthCare.Operations.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.MappingProfiles.AppointmentMappings
{
    public class AppointmentMappingProfile : Profile
    {
        public AppointmentMappingProfile()
        {
            CreateMap<Appointment, GetAppointmentsQueryResult>().ReverseMap();
            CreateMap<Appointment, GetAppointmentByIdQueryResult>().ReverseMap();
            CreateMap<Appointment, GetAppointmentsByFilterQueryResult>().ReverseMap();
            CreateMap<Appointment, CreateAppointmentCommand>().ReverseMap();
            CreateMap<Appointment, UpdateAppointmentCommand>().ReverseMap();
        }
    }
}
