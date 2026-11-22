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

            return await response.ReturnServiceResultAsync<IReadOnlyCollection<RoleResultDto>>();

            //string jsonData = await response.Content.ReadAsStringAsync();

            //ApiServiceResponse<IReadOnlyCollection<RoleResultDto>> serviceResponse = await response.Content.ReadFromJsonAsync<ApiServiceResponse<IReadOnlyCollection<RoleResultDto>>>();

            //return await serviceResponse.ReturnServiceResultAsync(response);
        }

        public async Task<UIApiResponse<RoleResultDto>> GetAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.GetAsync($"{Endpoint}?id={id}");

            return await response.ReturnServiceResultAsync<RoleResultDto>();

            //ApiServiceResponse<RoleResultDto> serviceResponse = await response.Content.ReadFromJsonAsync<ApiServiceResponse<RoleResultDto>>();

            //return await serviceResponse.ReturnServiceResultAsync(response);
        }

        public async Task<UIApiResponse<Guid>> CreateAsync(RoleCreateDto createDto)
        {

            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.PostAsJsonAsync(Endpoint, createDto);

            return await response.ReturnServiceResultAsync<Guid>();

            //string jsonData = await response.Content.ReadAsStringAsync();

            //ApiServiceResponse<Guid> serviceResponse = await response.Content.ReadFromJsonAsync<ApiServiceResponse<Guid>>();

            //return await serviceResponse.ReturnServiceResultAsync(response);
        }

        public async Task<UIApiResponse<DateTimeOffset>> UpdateAsync(RoleUpdateDto updateDto)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.PutAsJsonAsync(Endpoint, updateDto);

            return await response.ReturnServiceResultAsync<DateTimeOffset>();

            //ApiServiceResponse<DateTimeOffset> serviceResponse = await response.Content.ReadFromJsonAsync<ApiServiceResponse<DateTimeOffset>>();

            //return await serviceResponse.ReturnServiceResultAsync(response);
        }

        public async Task<UIApiResponse<bool>> DeleteAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.DeleteAsync($"{Endpoint}?id={id}");

            return await response.ReturnServiceResultAsync<bool>();

            //ApiServiceResponse<bool> serviceResponse = await response.Content.ReadFromJsonAsync<ApiServiceResponse<bool>>();

            //return await serviceResponse.ReturnServiceResultAsync(response);
        }
    }
}