using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Extensions;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Helpers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.ServiceType;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.ServiceTypes
{
    public class ServiceTypeCreatePolicy : PolicyRule<ServicingType>, IServiceTypeCreatePolicy
    {
        private readonly IServiceTypeQueryService _queryService;

        public ServiceTypeCreatePolicy(IServiceTypeQueryService queryService)
        {
            _queryService = queryService;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(ServicingType entity)
        {
            DBQueryOptions<ServicingType> dBQueryOptions = new DBQueryOptions<ServicingType>();
            Expression<Func<ServicingType, bool>> filter = x => (
                (x.ServiceCode == entity.ServiceCode) || (x.ServiceName == entity.ServiceName)
            );

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            return serviceResponse.ToPolicyResponse(x => x > 0, "Aynı Servisi Tipi kodu veya Adından Birden Fazla Olamaz!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(ServicingType entity)
        {
            PolicyResponseHelper createPolicy = new PolicyResponseHelper
            {
                ()=>CountExistingDataAsync(entity)
            };

            return await createPolicy.ToPolicyResponseAsync();
        }
    }
}