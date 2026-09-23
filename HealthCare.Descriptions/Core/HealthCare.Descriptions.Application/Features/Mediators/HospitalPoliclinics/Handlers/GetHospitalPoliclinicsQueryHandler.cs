using AutoMapper.Configuration.Annotations;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Helpers;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinics;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Handlers
{
    public class GetHospitalPoliclinicsQueryHandler : IRequestHandler<GetHospitalPoliclinicsQuery, InternalHandlerResponse<IReadOnlyCollection<GetHospitalPoliclinicsQueryResult>>>
    {
        private readonly IHospitalPoliclinicQueryService _queryService;
        private readonly ITokenBasedPaginationHelper<HospitalPoliclinic, GetHospitalPoliclinicsQueryHandler, GetHospitalPoliclinicsQueryResult, string> _paginationHelper;

        public GetHospitalPoliclinicsQueryHandler(IHospitalPoliclinicQueryService queryService, ITokenBasedPaginationHelper<HospitalPoliclinic, GetHospitalPoliclinicsQueryHandler, GetHospitalPoliclinicsQueryResult, string> paginationHelper)
        {
            _queryService = queryService;
            _paginationHelper = paginationHelper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetHospitalPoliclinicsQueryResult>>> Handle(GetHospitalPoliclinicsQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<HospitalPoliclinic> dBQueryOptions = new DBQueryOptions<HospitalPoliclinic>();

            dBQueryOptions.thenIncludes = new Dictionary<Expression<Func<HospitalPoliclinic, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        x=>x.Hospital,
                        new List<Expression<Func<object, object>>>{
                            y=>((Hospital)y).City
                        }
                    },
                    {
                        x=>x.Hospital,
                        new List<Expression<Func<object, object>>>{
                            z=>((Hospital)z).District
                        }
                    },
                    {
                        x=>x.Policlinic,
                        new List<Expression<Func<object, object>>>{ }
                    }
                };

            dBQueryOptions.thenOrderBy = new Dictionary<Expression<Func<HospitalPoliclinic, object>>, List<Expression<Func<HospitalPoliclinic, object>>>>
            {
                {
                    x=>x.Id,
                    new List<Expression<Func<HospitalPoliclinic, object>>>
                    {
                        y=>y.Hospital.HospitalName,
                        y=>y.Policlinic.PoliclinicName
                    }
                }
            };

            dBQueryOptions.thenBySortingType = 0;

            var config = new TokenPayloadConfig<HospitalPoliclinic, GetHospitalPoliclinicsQueryHandler, GetHospitalPoliclinicsQueryResult, string>
            {
                Token = request.Token,

                OrderBy = x => x.Hospital.HospitalName,
                ForwardFilter = lastName => x => string.Compare(x.Hospital.HospitalName, lastName) > 0,
                BackwardFilter = firstName => x => string.Compare(x.Hospital.HospitalName, firstName) < 0,

                CursorSelector = x => x.Hospital.HospitalName,
                CreatedAtSelector = x => x.CreatedAt,

                GetTotalCountAsync = async () =>
                {
                    InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync();

                    return serviceResponse.Data;
                },

                FetchDataAsync = async (options) =>
                {
                    InternalServiceResponse<IReadOnlyCollection<GetHospitalPoliclinicsQueryResult>> serviceResponse =
                        await _queryService.GetDatasAsync<GetHospitalPoliclinicsQueryResult>(options);

                    return serviceResponse;
                }
            };

            return await _paginationHelper.PaginationResultAsync(config, dBQueryOptions);

            //InternalServiceResponse<IReadOnlyCollection<GetHospitalPoliclinicsQueryResult>> serviceResponse =
            //    await _queryService.GetDatasAsync<GetHospitalPoliclinicsQueryResult>(dBQueryOptions);

            //return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}