using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Extensions;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Helpers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.QuotaTypes;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.QuotaTypes
{
    public class QuotaTypeUpdatePolicy : PolicyRule<QuotaType>, IQuotaTypeUpdatePolicy
    {
        private readonly IQuotaTypeQueryService _queryService;
        private readonly IQuotaTypeCreatePolicy _createPolicy;

        public QuotaTypeUpdatePolicy(IQuotaTypeQueryService queryService, IQuotaTypeCreatePolicy createPolicy)
        {
            _queryService = queryService;
            _createPolicy = createPolicy;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(QuotaType entity)
        {
            DBQueryOptions<QuotaType> dBQueryOptions = new DBQueryOptions<QuotaType>();
            Expression<Func<QuotaType, bool>> filter = x => (x.Id == entity.Id);
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            return serviceResponse.ToPolicyResponse(x => x == 0, "Güncellenecek Id'ye Ait Kota Tipi Verisi Bulunamadı!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(QuotaType entity)
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