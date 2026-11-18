using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicRoleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Persistence.Commons.Extensions
{
    public static class ApiServiceResponseExtension
    {
        public static async Task<UIApiResponse<T>> ReturnServiceResult<T>(this ApiServiceResponse<T> apiResponse, HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (apiResponse.IsSuccess)

                    return UIApiResponse<T>.Success(apiResponse.Data);
                else

                    return UIApiResponse<T>.Fail(apiResponse.Message);
            }

            return await UIApiResponse<T>.ErrorAsync(response);
        }
    }
}