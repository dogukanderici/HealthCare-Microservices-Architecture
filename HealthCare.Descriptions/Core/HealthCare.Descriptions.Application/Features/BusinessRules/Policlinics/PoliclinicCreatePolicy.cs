using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Extensions;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Helpers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Policlinics;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.Policlinics
{
    public class PoliclinicCreatePolicy : PolicyRule<Policlinic>, IPoliclinicCreatePolicy
    {
        private readonly IPoliclinicQueryService _queryService;

        public PoliclinicCreatePolicy(IPoliclinicQueryService queryService)
        {
            _queryService = queryService;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(Policlinic entity)
        {
            DBQueryOptions<Policlinic> dBQueryOptions = new DBQueryOptions<Policlinic>();
            Expression<Func<Policlinic, bool>> filter = x => (
                ((x.PoliclinicCode == entity.PoliclinicCode) || (x.PoliclinicName == entity.PoliclinicName)) && (x.Id != entity.Id)
            );
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResult = await _queryService.GetDataCountAsync(dBQueryOptions);

            // Extension metota kontrol kuralı ve kontrolden geçmemesi halinde dönecek mesaj gönderilir.
            return serviceResult.ToPolicyResponse(x => x > 0, "Aynı Policlinic Kodu veya İsminden Birden Fazla Olamaz!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(Policlinic entity)
        {
            PolicyResponseHelper createPolicy = new PolicyResponseHelper
            {
                ()=>CountExistingDataAsync(entity)
            };

            return await createPolicy.ToPolicyResponseAsync();
        }
    }
}