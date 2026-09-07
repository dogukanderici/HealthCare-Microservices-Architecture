using HealthCare.Descriptions.Application.Behaviors;
using HealthCare.Descriptions.Application.Features.Mediators;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Configuration.Extentions
{
    public static class MediatorRegistration
    {
        public static IServiceCollection AddMediatorRegistration(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));

                cfg.RegisterServicesFromAssembly(typeof(CreateAppointmentStatusCommandHandler).Assembly);
            });


            return services;
        }
    }
}