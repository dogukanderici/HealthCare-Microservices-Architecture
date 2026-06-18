using AutoMapper;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.CityCommands;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.CityHandlers
{
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand>
    {

        private readonly IRepository<City> _repository;
        private readonly IMapper _mapper;

        public DeleteCityCommandHandler(IRepository<City> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            DbQueryOptions<City> dbQueryOptions = new DbQueryOptions<City>();

            Expression<Func<City, bool>> filter = c => c.Id == request.Id;

            dbQueryOptions.filter = filter;

            City result = await _repository.GetDataAsync();

            if (result != null)
            {
                await _repository.DeleteDataAsync(result);
            }
        }
    }
}
