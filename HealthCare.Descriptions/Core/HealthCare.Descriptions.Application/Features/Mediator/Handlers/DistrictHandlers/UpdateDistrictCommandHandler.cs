using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.DistrictCommands;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.DistrictHandlers
{
    public class UpdateDistrictCommandHandler : IRequestHandler<UpdateDistrictCommand>
    {

        private readonly IRepository<District> _repository;
        private readonly IMapper _mapper;

        public UpdateDistrictCommandHandler(IRepository<District> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateDistrictCommand request, CancellationToken cancellationToken)
        {
            District dataFromDto = _mapper.Map<District>(request);

            await _repository.UpdateDataAsync(dataFromDto);
        }
    }
}
