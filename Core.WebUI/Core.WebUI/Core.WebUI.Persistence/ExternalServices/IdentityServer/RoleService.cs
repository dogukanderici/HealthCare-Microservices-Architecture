using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.IdentityDtos.RoleDtos;
using Core.WebUI.Application.Interfaces.Services.IdentityServer.RoleService;
using Core.WebUI.Persistence.Commons.Extensions;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Core.WebUI.Persistence.ExternalServices.IdentityServer
{
    public class RoleService : BaseService, IRoleService
    {
        protected override string ServiceBaseUrl { get; set; }
        protected override string Endpoint { get; set; }
        protected override string HttpClientName { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public RoleService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            ServiceBaseUrl = _configuration.GetSection("ApiSetting:IdentityServerBaseUrl").Value;
            Endpoint = _configuration.GetSection("ApiSetting:IdentityServer:APIUrl:Role").Value;
            //HttpClientName = _configuration.GetSection("ApiSetting:HttpClientName").Value;
            HttpClientName = _configuration.GetSection("ApiSetting:IdentityServer:APIClientName").Value;
        }

        public async Task<UIApiResponse<IReadOnlyCollection<RoleResultDto>>> GetListAsync(Guid? id = null)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.GetAsync(Endpoint);

            ApiServiceResponse<IReadOnlyCollection<RoleResultDto>> serviceResponse = await response.Content.ReadFromJsonAsync<ApiServiceResponse<IReadOnlyCollection<RoleResultDto>>>();

            return await serviceResponse.ReturnServiceResultAsync(response);
        }

        public async Task<UIApiResponse<RoleResultDto>> GetAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.GetAsync($"{Endpoint}?id={id}");

            ApiServiceResponse<RoleResultDto> serviceResponse = await response.Content.ReadFromJsonAsync<ApiServiceResponse<RoleResultDto>>();

            return await serviceResponse.ReturnServiceResultAsync(response);
        }

        public async Task<UIApiResponse<bool>> CreateAsync(RoleCreateDto createDto)
        {

            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.PostAsJsonAsync(Endpoint, createDto);

            ApiServiceResponse<bool> serviceResponse = await response.Content.ReadFromJsonAsync<ApiServiceResponse<bool>>();

            return await serviceResponse.ReturnServiceResultAsync(response);
        }

        public async Task<UIApiResponse<bool>> UpdateAsync(RoleUpdateDto updateDto)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.PutAsJsonAsync(Endpoint, updateDto);

            ApiServiceResponse<bool> serviceResponse = await response.Content.ReadFromJsonAsync<ApiServiceResponse<bool>>();

            return await serviceResponse.ReturnServiceResultAsync(response);
        }

        public async Task<UIApiResponse<bool>> DeleteAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.DeleteAsync($"{Endpoint}?id={id}");

            ApiServiceResponse<bool> serviceResponse = await response.Content.ReadFromJsonAsync<ApiServiceResponse<bool>>();

            return await serviceResponse.ReturnServiceResultAsync(response);
        }
    }
}