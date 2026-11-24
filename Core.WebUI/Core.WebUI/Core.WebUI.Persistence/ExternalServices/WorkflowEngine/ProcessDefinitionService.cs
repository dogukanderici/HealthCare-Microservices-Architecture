using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessDefinitionDtos;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Core.WebUI.Persistence.Commons.Extensions;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Core.WebUI.Persistence.ExternalServices.WorkflowEngine
{
    public class ProcessDefinitionService : BaseService, IProcessDefinitionService
    {
        protected override string ServiceBaseUrl { get; set; }
        protected override string Endpoint { get; set; }
        protected override string HttpClientName { get; set; }

        private readonly IConfiguration _config;

        private readonly IHttpClientFactory _httpClientFactory;

        public ProcessDefinitionService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;

            ServiceBaseUrl = _config.GetSection("ApiSetting:WorkflowEngine:ServiceBaseUrl").Value;
            Endpoint = _config.GetSection("ApiSetting:WorkflowEngine:APIUrl:ProcessDefinition").Value;
            HttpClientName = _config.GetSection("ApiSetting:ApiHttpClientName").Value;
        }

        public async Task<UIApiResponse<IReadOnlyCollection<ProcessDefinitionResultDto>>> GetListAsync(Guid? id = null)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.GetAsync(Endpoint);

            return await response.ReturnServiceResultAsync<IReadOnlyCollection<ProcessDefinitionResultDto>>();
        }

        public async Task<UIApiResponse<ProcessDefinitionResultDto>> GetAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.GetAsync($"{Endpoint}/{id}");

            return await response.ReturnServiceResultAsync<ProcessDefinitionResultDto>();
        }

        public async Task<UIApiResponse<Guid>> CreateAsync(ProcessDefinitionCreateDto dto)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.PostAsJsonAsync(Endpoint, dto);

            return await response.ReturnServiceResultAsync<Guid>();
        }

        public async Task<UIApiResponse<DateTimeOffset>> UpdateAsync(ProcessDefinitionUpdateDto dto)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.PutAsJsonAsync(Endpoint, dto);

            return await response.ReturnServiceResultAsync<DateTimeOffset>();
        }

        public async Task<UIApiResponse<bool>> DeleteAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.DeleteAsync($"{Endpoint}/{id}");

            return await response.ReturnServiceResultAsync<bool>();
        }
    }
}