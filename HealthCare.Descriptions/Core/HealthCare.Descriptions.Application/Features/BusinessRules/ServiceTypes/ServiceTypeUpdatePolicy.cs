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
    public class ServiceTypeUpdatePolicy : PolicyRule<ServicingType>, IServiceTypeUpdatePolicy
    {
        private readonly IServiceTypeQueryService _queryService;
        private readonly IServiceTypeCreatePolicy _createPolicy;

        public ServiceTypeUpdatePolicy(IServiceTypeQueryService queryService, IServiceTypeCreatePolicy createPolicy)
        {
            _queryService = queryService;
            _createPolicy = createPolicy;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(ServicingType entity)
        {
            DBQueryOptions<ServicingType> dBQueryOptions = new DBQueryOptions<ServicingType>();
            Expression<Func<ServicingType, bool>> filter = x => (x.Id == entity.Id);

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            return serviceResponse.ToPolicyResponse(x => x == 0, "Güncellenecek Id'ye Ait Servis Tipi Verisi Bulunamadı!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(ServicingType entity)
        {
            PolicyResponseHelper updatePolicy = new PolicyResponseHelper
            {
                ()=>_createPolicy.ExecuteAllRulesAsync(entity),
                ()=>CountExistingDataAsync(entity)
            };

            return await updatePolicy.ToPolicyResponseAsync();
        }
    }
}