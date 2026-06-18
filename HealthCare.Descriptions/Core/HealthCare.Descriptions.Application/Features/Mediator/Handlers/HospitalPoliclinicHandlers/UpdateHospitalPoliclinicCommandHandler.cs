using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalPoliclinicCommands;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.HospitalPoliclinicHandlers
{
    public class UpdateHospitalPoliclinicCommandHandler : IRequestHandler<UpdateHospitalPoliclinicCommand>
    {


        private readonly IRepository<HospitalPoliclinic> _repository;
        private readonly IMapper _mapper;

        public UpdateHospitalPoliclinicCommandHandler(IRepository<HospitalPoliclinic> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateHospitalPoliclinicCommand request, CancellationToken cancellationToken)
        {
            HospitalPoliclinic dataFromDto = _mapper.Map<HospitalPoliclinic>(request);

            await _repository.UpdateDataAsync(dataFromDto);
        }
    }
}
