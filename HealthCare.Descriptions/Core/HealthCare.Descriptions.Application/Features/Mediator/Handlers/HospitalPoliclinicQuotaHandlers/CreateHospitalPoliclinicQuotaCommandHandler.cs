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
    public class CreateHospitalPoliclinicQuotaCommandHandler : IRequestHandler<CreateHospitalPoliclinicQuotaCommand>
    {

        private readonly IRepository<HospitalPoliclinicQuota> _repository;
        private readonly IMapper _mapper;

        public CreateHospitalPoliclinicQuotaCommandHandler(IRepository<HospitalPoliclinicQuota> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateHospitalPoliclinicQuotaCommand request, CancellationToken cancellationToken)
        {
            HospitalPoliclinicQuota dataFromDto = _mapper.Map<HospitalPoliclinicQuota>(request);

            await _repository.CreateDataAsync(dataFromDto);
        }
    }
}
