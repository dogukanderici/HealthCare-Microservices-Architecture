using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicPolicyDtos;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Core.WebUI.Persistence.ExternalServices.WorkflowEngine
{
    public class DynamicPolicyService : BaseService, IDynamicPolicyService
    {

        protected override string ServiceBaseUrl { get; set; }
        protected override string HttpClientName { get; set; }
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public DynamicPolicyService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            ServiceBaseUrl = _configuration.GetSection("ApiSetting:WorkflowEngine:ServiceBaseUrl").Value;
            HttpClientName = _configuration.GetSection("ApiSetting:ApiHttpClientName").Value;
        }

        public async Task<UIApiResponse<IReadOnlyCollection<DynamicPolicyResultDto>>> GetListAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.GetAsync("dynamicpolicies");

            string jsonData = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {

                ApiServiceResponse<IReadOnlyCollection<DynamicPolicyResultDto>> apiServiceResponse =
                    JsonConvert.DeserializeObject<ApiServiceResponse<IReadOnlyCollection<DynamicPolicyResultDto>>>(jsonData);

                if (apiServiceResponse.IsSuccess)
                {
                    return UIApiResponse<IReadOnlyCollection<DynamicPolicyResultDto>>.Success(apiServiceResponse.Data);
                }

                return UIApiResponse<IReadOnlyCollection<DynamicPolicyResultDto>>.Fail(apiServiceResponse.Message);
            }

            return await UIApiResponse<IReadOnlyCollection<DynamicPolicyResultDto>>.ErrorAsync(response);
        }

        public async Task<UIApiResponse<DynamicPolicyResultDto>> GetAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.GetAsync($"dynamicpolicies/{id}");

            string jsonData = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {

                ApiServiceResponse<DynamicPolicyResultDto> apiServiceResponse =
                    JsonConvert.DeserializeObject<ApiServiceResponse<DynamicPolicyResultDto>>(jsonData);

                if (apiServiceResponse.IsSuccess)
                    return UIApiResponse<DynamicPolicyResultDto>.Success(apiServiceResponse.Data);

                return UIApiResponse<DynamicPolicyResultDto>.Fail(apiServiceResponse.Message);
            }

            return await UIApiResponse<DynamicPolicyResultDto>.ErrorAsync(response);
        }

        public async Task<UIApiResponse<bool>> CreateAsync(DynamicPolicyCreateDto createDto)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.PostAsJsonAsync("dynamicpolicies", createDto);

            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                ApiServiceResponse<Guid> apiResponse = JsonConvert.DeserializeObject<ApiServiceResponse<Guid>>(responseContent);

                if (apiResponse.IsSuccess)
                    return UIApiResponse<bool>.Success(true);
                else
                    return UIApiResponse<bool>.Fail(apiResponse.Message);
            }

            return await UIApiResponse<bool>.ErrorAsync(response);
        }

        public async Task<UIApiResponse<bool>> UpdateAsync(DynamicPolicyUpdateDto updateDto)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.PutAsJsonAsync("dynamicpolicies", updateDto);

            string strData = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {

                ApiServiceResponse<DateTimeOffset> apiServiceResponse = JsonConvert.DeserializeObject<ApiServiceResponse<DateTimeOffset>>(strData);

                if (apiServiceResponse.IsSuccess)
                    return UIApiResponse<bool>.Success(true);

                return UIApiResponse<bool>.Fail(apiServiceResponse.Message);
            }

            return await UIApiResponse<bool>.ErrorAsync(response);
        }

        public async Task<UIApiResponse<bool>> DeleteAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.DeleteAsync($"dynamicpolicies/{id}");

            string strData = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {

                ApiServiceResponse<bool> apiServiceResponse = JsonConvert.DeserializeObject<ApiServiceResponse<bool>>(strData);

                if (apiServiceResponse.IsSuccess)
                    return UIApiResponse<bool>.Success(true);

                return UIApiResponse<bool>.Fail(apiServiceResponse.Message);
            }

            return await UIApiResponse<bool>.ErrorAsync(response);
        }
    }
}