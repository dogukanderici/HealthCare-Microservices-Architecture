using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.AppointmentStatusCommands;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.AppointmentStatusHandlers
{
    public class DeleteAppointmentStatusCommandHandler : IRequestHandler<DeleteAppointmentStatusCommand>
    {
        private readonly IRepository<AppointmentStatus> _repository;

        public DeleteAppointmentStatusCommandHandler(IRepository<AppointmentStatus> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteAppointmentStatusCommand request, CancellationToken cancellationToken)
        {
            DbQueryOptions<AppointmentStatus> dbQueryOptions = new DbQueryOptions<AppointmentStatus>();

            Expression<Func<AppointmentStatus, bool>> filter = apps => apps.Id == request.Id;
            dbQueryOptions.filter = filter;

            AppointmentStatus result = await _repository.GetDataAsync(dbQueryOptions);

            if (result != null)
            {
                await _repository.DeleteDataAsync(result);
            }
        }
    }
}
