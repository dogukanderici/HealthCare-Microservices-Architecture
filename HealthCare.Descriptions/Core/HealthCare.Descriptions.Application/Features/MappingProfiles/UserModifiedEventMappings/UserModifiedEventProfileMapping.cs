using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.UserModifiedEventCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Results.UserModifiedEventResults;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.MappingProfiles.UserModifiedEventMappings
{
    public class UserModifiedEventProfileMapping : Profile
    {
        public UserModifiedEventProfileMapping()
        {
            CreateMap<UserModifiedEvent, GetUserModifiedEventsQueryResult>().ReverseMap();
            CreateMap<UserModifiedEvent, GetUserModifiedEventByIdQueryResult>().ReverseMap();
            CreateMap<UserModifiedEvent, GetUserModifiedEventsByFilterQueryResult>().ReverseMap();
            CreateMap<UserModifiedEvent, CreateUserModifiedEventCommand>().ReverseMap();
            CreateMap<GetUserModifiedEventByIdQueryResult, CreateUserModifiedEventCommand>().ReverseMap();
            CreateMap<UserModifiedEvent, UpdateUserModifiedEventCommand>().ReverseMap();
            CreateMap<GetUserModifiedEventByIdQueryResult, UpdateUserModifiedEventCommand>().ReverseMap();
        }
    }
}
