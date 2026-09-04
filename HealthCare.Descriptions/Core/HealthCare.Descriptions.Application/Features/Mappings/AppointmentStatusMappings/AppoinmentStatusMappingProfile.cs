using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Results;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mappings.AppointmentStatusMappings
{
    public class AppoinmentStatusMappingProfile : Profile
    {
        public AppoinmentStatusMappingProfile()
        {
            CreateMap<AppointmentStatus, GetAppointmentStatusesResult>().ReverseMap();
            CreateMap<AppointmentStatus, GetAppointmentStatusByIdResult>().ReverseMap();
        }
    }
}