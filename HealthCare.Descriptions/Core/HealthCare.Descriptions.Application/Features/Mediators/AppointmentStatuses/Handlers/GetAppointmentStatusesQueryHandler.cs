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
        private readonly ITokenBasedPaginationHelper<AppointmentStatus, GetAppointmentStatusesQueryHandler, GetAppointmentStatusesResult, string> _paginationHelper;

        public GetAppointmentStatusesQueryHandler(IAppointmentStatusQueryService service, ITokenBasedPaginationHelper<AppointmentStatus, GetAppointmentStatusesQueryHandler, GetAppointmentStatusesResult, string> paginationHelper)
        {
            _service = service;
            _paginationHelper = paginationHelper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetAppointmentStatusesResult>>> Handle(GetAppointmentStatusesQuery request, CancellationToken cancellationToken)
        {
            var config = new TokenPayloadConfig<AppointmentStatus, GetAppointmentStatusesQueryHandler, GetAppointmentStatusesResult, string>
            {
                Token = request.Token,

                OrderBy = x => x.StatusName,
                ForwardFilter = lastName => x => string.Compare(x.StatusName, lastName) > 0,
                BackwardFilter = firstName => x => string.Compare(x.StatusName, firstName) < 0,

                CursorSelector = x => x.StatusName,
                CreatedAtSelector = x => x.CreatedAt,

                GetTotalCountAsync = async (options) =>
                {
                    InternalServiceResponse<int> serviceResult = await _service.GetDataCountAsync(options);
                    return serviceResult.Data;
                },

                FetchDataAsync = async (options) =>
                {
                    InternalServiceResponse<IReadOnlyCollection<GetAppointmentStatusesResult>> serviceResult =
                        await _service.GetDatasAsync<GetAppointmentStatusesResult>(options);

                    return serviceResult;
                }
            };

            return await _paginationHelper.PaginationResultAsync(config);

            //DBQueryOptions<AppointmentStatus> dBQueryOptions = new DBQueryOptions<AppointmentStatus>();

            //InternalServiceResponse<IReadOnlyCollection<GetAppointmentStatusesResult>> result = await _service.GetDatasAsync<GetAppointmentStatusesResult>(dBQueryOptions);

            //return InternalHandlerResponse<IReadOnlyCollection<GetAppointmentStatusesResult>>.Success(result.Data);
        }
    }
}