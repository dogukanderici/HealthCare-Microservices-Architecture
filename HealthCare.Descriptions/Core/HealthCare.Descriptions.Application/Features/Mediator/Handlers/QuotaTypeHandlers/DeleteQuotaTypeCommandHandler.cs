using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.QuotaTypeCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.QuotaTypeQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.QuotaTypeResults;
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
    public class DeleteQuotaTypeCommandHandler : IRequestHandler<DeleteQuotaTypeCommand>
    {
        private readonly IRepository<QuotaType> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeleteQuotaTypeCommandHandler(IRepository<QuotaType> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task Handle(DeleteQuotaTypeCommand request, CancellationToken cancellationToken)
        {
            GetQuotaTypeByIdQueryResult result = await _mediator.Send(new GetQuotaTypeByIdQuery(request.Id));

            if (result != null)
            {
                await _repository.DeleteDataAsync(_mapper.Map<QuotaType>(result));
            }
        }
    }
}
