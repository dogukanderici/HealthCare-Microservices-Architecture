using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Helpers;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.AppointmentStatutes;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Handlers
{
    public class GetAppointmentStatusesQueryHandler : IRequestHandler<GetAppointmentStatusesQuery, InternalHandlerResponse<IReadOnlyCollection<GetAppointmentStatusesResult>>>
    {
        private readonly IAppointmentStatusQueryService _service;

        public GetAppointmentStatusesQueryHandler(IAppointmentStatusQueryService service)
        {
            _service = service;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetAppointmentStatusesResult>>> Handle(GetAppointmentStatusesQuery request, CancellationToken cancellationToken)
        {
            var config = new TokenPayloadConfig<AppointmentStatus, GetAppointmentStatusesQueryHandler, GetAppointmentStatusesResult, DateTimeOffset>
            {
                Token = request.Token,

                OrderBy = x => x.StatusName,
                ForwardFilter = lastCreatedDate => x => x.CreatedAt > lastCreatedDate,
                BackwardFilter = firstCreatedDate => x => x.CreatedAt < firstCreatedDate,

                CursorSelector = x => x.CreatedAt,
                CreatedAtSelector = x => x.CreatedAt,

                GetTotalCountAsync = async () =>
                {
                    InternalServiceResponse<int> serviceResult = await _service.GetDataCountAsync();
                    return serviceResult.Data;
                },

                FetchDataAsync = async (options) =>
                {
                    InternalServiceResponse<IReadOnlyCollection<GetAppointmentStatusesResult>> serviceResult =
                        await _service.GetDatasAsync<GetAppointmentStatusesResult>(options);

                    return serviceResult;
                }
            };

            return await TokenBasedPaginationHelper.PaginationResultAsync(config);

            //DBQueryOptions<AppointmentStatus> dBQueryOptions = new DBQueryOptions<AppointmentStatus>();

            //InternalServiceResponse<IReadOnlyCollection<GetAppointmentStatusesResult>> result = await _service.GetDatasAsync<GetAppointmentStatusesResult>(dBQueryOptions);

            //return InternalHandlerResponse<IReadOnlyCollection<GetAppointmentStatusesResult>>.Success(result.Data);
        }
    }
}