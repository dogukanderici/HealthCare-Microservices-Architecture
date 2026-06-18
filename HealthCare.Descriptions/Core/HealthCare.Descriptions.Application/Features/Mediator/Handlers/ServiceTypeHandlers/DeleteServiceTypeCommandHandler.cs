using AutoMapper;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.ServiceCommands;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.ServiceHandlers
{
    public class DeleteServiceTypeCommandHandler : IRequestHandler<DeleteServiceTypeCommand>
    {

        private readonly IRepository<ServiceType> _repository;

        public DeleteServiceTypeCommandHandler(IRepository<ServiceType> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteServiceTypeCommand request, CancellationToken cancellationToken)
        {
            DbQueryOptions<ServiceType> dbQueryOptions = new DbQueryOptions<ServiceType>();

            Expression<Func<ServiceType, bool>> filter = st => st.Id == request.Id;
            dbQueryOptions.filter = filter;

            ServiceType result = await _repository.GetDataAsync(dbQueryOptions);

            if (result != null)
            {
                await _repository.DeleteDataAsync(result);
            }
        }
    }
}
