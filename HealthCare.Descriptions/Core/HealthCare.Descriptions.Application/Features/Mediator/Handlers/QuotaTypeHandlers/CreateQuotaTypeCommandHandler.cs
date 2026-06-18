using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.QuotaTypeCommands;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.QuotaTypeHandlers
{
    public class CreateQuotaTypeCommandHandler : IRequestHandler<CreateQuotaTypeCommand>
    {
        private readonly IRepository<QuotaType> _repository;
        private readonly IMapper _mapper;

        public CreateQuotaTypeCommandHandler(IRepository<QuotaType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateQuotaTypeCommand request, CancellationToken cancellationToken)
        {
            QuotaType dataFromDto = _mapper.Map<QuotaType>(request);

            await _repository.CreateDataAsync(dataFromDto);
        }
    }
}
