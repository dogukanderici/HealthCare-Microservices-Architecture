using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicRoleDtos;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Core.WebUI.Persistence.Commons.Extensions;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Core.WebUI.Persistence.ExternalServices.WorkflowEngine
{
    public class DynamicRoleService : BaseService, IDynamicRoleService
    {
        protected override string ServiceBaseUrl { get; set; }
        protected override string Endpoint { get; set; }
        protected override string HttpClientName { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public DynamicRoleService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            ServiceBaseUrl = _configuration.GetSection("ApiSetting:WorkflowEngine:ServiceBaseUrl").Value;
            Endpoint = _configuration.GetSection("ApiSetting:WorkflowEngine:APIUrl:DynamicRole").Value;
            HttpClientName = _configuration.GetSection("ApiSetting:ApiHttpClientName").Value;
        }

        public async Task<UIApiResponse<IReadOnlyCollection<DynamicRoleResultDto>>> GetListAsync(Guid? id = null)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.GetAsync($"{Endpoint}/policyid?policyid={id}");

            return await response.ReturnServiceResultAsync<IReadOnlyCollection<DynamicRoleResultDto>>();

            //ApiServiceResponse<IReadOnlyCollection<DynamicRoleResultDto>> apiServiceResponse =
            //    await response.Content.ReadFromJsonAsync<ApiServiceResponse<IReadOnlyCollection<DynamicRoleResultDto>>>();

            //return await apiServiceResponse.ReturnApiResponseAsync(response);

            //if (response.IsSuccessStatusCode)
            //{
            //    if (apiServiceResponse.IsSuccess)

            //        return UIApiResponse<IReadOnlyCollection<DynamicRoleResultDto>>.Success(apiServiceResponse.Data);
            //    else

            //        return UIApiResponse<IReadOnlyCollection<DynamicRoleResultDto>>.Fail(apiServiceResponse.Message);
            //}

            //return await UIApiResponse<IReadOnlyCollection<DynamicRoleResultDto>>.ErrorAsync(response);
        }

        public async Task<UIApiResponse<IReadOnlyCollection<DynamicRoleResultDto>>> GetListWithFilterAsync(DynamicRoleQueryDto filter)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.PostAsJsonAsync($"{Endpoint}/filter", filter);

            return await response.ReturnServiceResultAsync<IReadOnlyCollection<DynamicRoleResultDto>>();

            //ApiServiceResponse<IReadOnlyCollection<DynamicRoleResultDto>> apiServiceResponse =
            //    await response.Content.ReadFromJsonAsync<ApiServiceResponse<IReadOnlyCollection<DynamicRoleResultDto>>>();

            //return await apiServiceResponse.ReturnServiceResultAsync(response);
        }

        public async Task<UIApiResponse<DynamicRoleResultDto>> GetAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.GetAsync($"{Endpoint}/{id}");

            return await response.ReturnServiceResultAsync<DynamicRoleResultDto>();

            //ApiServiceResponse<DynamicRoleResultDto> apiServiceResponse =
            //    await response.Content.ReadFromJsonAsync<ApiServiceResponse<DynamicRoleResultDto>>();

            //return await apiServiceResponse.ReturnServiceResultAsync(response);
        }

        public async Task<UIApiResponse<Guid>> CreateAsync(DynamicRoleCreateDto createDto)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.PostAsJsonAsync(Endpoint, createDto);

            return await response.ReturnServiceResultAsync<Guid>();

            //ApiServiceResponse<Guid> apiServiceResponse =
            //    await response.Content.ReadFromJsonAsync<ApiServiceResponse<Guid>>();

            //return await apiServiceResponse.ReturnServiceResultAsync(response);
        }

        public async Task<UIApiResponse<DateTimeOffset>> UpdateAsync(DynamicRoleUpdateDto updateDto)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.PutAsJsonAsync(Endpoint, updateDto);

            return await response.ReturnServiceResultAsync<DateTimeOffset>();

            //ApiServiceResponse<DateTimeOffset> apiServiceResponse =
            //    await response.Content.ReadFromJsonAsync<ApiServiceResponse<DateTimeOffset>>();

            //return await apiServiceResponse.ReturnServiceResultAsync(response);
        }

        public async Task<UIApiResponse<bool>> DeleteAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.DeleteAsync($"{Endpoint}/{id}");

            return await response.ReturnServiceResultAsync<bool>();

            //ApiServiceResponse<bool> apiServiceResponse =
            //    await response.Content.ReadFromJsonAsync<ApiServiceResponse<bool>>();

            //return await apiServiceResponse.ReturnServiceResultAsync(response);
        }
    }
}