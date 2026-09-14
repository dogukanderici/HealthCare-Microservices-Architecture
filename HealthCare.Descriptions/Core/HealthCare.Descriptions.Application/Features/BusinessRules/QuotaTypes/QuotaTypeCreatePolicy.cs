using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Extensions;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Helpers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.QuotaTypes;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.QuotaTypes
{
    public class QuotaTypeCreatePolicy : PolicyRule<QuotaType>, IQuotaTypeCreatePolicy
    {
        private readonly IQuotaTypeQueryService _queryService;

        public QuotaTypeCreatePolicy(IQuotaTypeQueryService queryService)
        {
            _queryService = queryService;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(QuotaType entity)
        {
            DBQueryOptions<QuotaType> dBQueryOptions = new DBQueryOptions<QuotaType>();
            Expression<Func<QuotaType, bool>> filter = x => (
                (x.QuotaTypeCode == entity.QuotaTypeCode) || (x.QuotaTypeName == entity.QuotaTypeName)
            );
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            return serviceResponse.ToPolicyResponse(x => x > 0, "Aynı Kota Tipi Tanımından Birden Fazla Olamaz!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(QuotaType entity)
        {
            PolicyResponseHelper createPolicy = new PolicyResponseHelper
            {
                ()=>CountExistingDataAsync(entity)
            };

            return await createPolicy.ToPolicyResponseAsync();
        }
    }
}