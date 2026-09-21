using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Wrappers.Helpers
{
    public interface ITokenBasedPaginationHelper<T, THandler, TDto, TPropType>
        where T : class
    {
        Task<InternalHandlerResponse<IReadOnlyCollection<TDto>>> PaginationResultAsync(TokenPayloadConfig<T, THandler, TDto, TPropType> config, DBQueryOptions<T>? dBQueryOptions = null);
    }
}