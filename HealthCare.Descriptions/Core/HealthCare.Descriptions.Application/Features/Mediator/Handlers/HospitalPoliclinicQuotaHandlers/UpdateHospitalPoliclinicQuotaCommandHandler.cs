using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalPoliclinicQuotaCommands;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.HospitalPoliclinicQuotaHandlers
{
    public class UpdateHospitalPoliclinicQuotaCommandHandler : IRequestHandler<UpdateHospitalPoliclinicQuotaCommand>
    {

        private readonly IRepository<HospitalPoliclinicQuota> _repository;
        private readonly IMapper _mapper;

        public UpdateHospitalPoliclinicQuotaCommandHandler(IRepository<HospitalPoliclinicQuota> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateHospitalPoliclinicQuotaCommand request, CancellationToken cancellationToken)
        {
            HospitalPoliclinicQuota dataFromDto = _mapper.Map<HospitalPoliclinicQuota>(request);

            await _repository.UpdateDataAsync(dataFromDto);
        }
    }
}
