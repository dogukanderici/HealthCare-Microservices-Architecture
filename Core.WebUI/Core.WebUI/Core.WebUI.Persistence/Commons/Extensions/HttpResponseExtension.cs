using Core.WebUI.Application.Commons.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Persistence.Commons.Extensions
{
    public static class HttpResponseExtension
    {
        public static async Task<UIApiResponse<T>> ReturnServiceResultAsync<T>(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                ApiServiceResponse<T> apiServiceResponse = await response.Content.ReadFromJsonAsync<ApiServiceResponse<T>>();

                return await apiServiceResponse.ReturnApiResponseAsync(response);
            }

            return UIApiResponse<T>.Fail($"StatusCode: {(int)response.StatusCode} ** Message: {response.ReasonPhrase}");
        }
    }
}