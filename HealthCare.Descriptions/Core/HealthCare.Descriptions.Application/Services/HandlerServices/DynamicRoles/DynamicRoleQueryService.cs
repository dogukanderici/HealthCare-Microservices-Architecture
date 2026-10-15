using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.DynamicRoles;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.DynamicRoles
{
    public class DynamicRoleQueryService : IDynamicRoleQueryService
    {

        public Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<DynamicRole>? options = null) where TResult : IListResult
        {
            throw new NotImplementedException();
        }

        public Task<InternalServiceResponse<TResult>> GetDataAsync<TResult>(Guid id) where TResult : ISingleResult
        {
            throw new NotImplementedException();
        }

        public Task<InternalServiceResponse<int>> GetDataCountAsync(DBQueryOptions<DynamicRole>? options = null)
        {
            throw new NotImplementedException();
        }
    }
}